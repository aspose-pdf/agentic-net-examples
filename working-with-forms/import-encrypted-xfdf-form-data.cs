using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string encryptedXmlPath = "formdata.xml.enc";
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        // -----------------------------------------------------------------
        // 1. Decrypt the encrypted XML file (AES‑CBC example)
        // -----------------------------------------------------------------
        // NOTE: Replace the placeholder key/IV with the actual values used
        // for encryption. Both must be the correct length (32 bytes for a
        // 256‑bit key, 16 bytes for a 128‑bit IV).
        byte[] key = new byte[32]; // TODO: populate with real key
        byte[] iv  = new byte[16]; // TODO: populate with real IV

        if (!File.Exists(encryptedXmlPath))
        {
            Console.Error.WriteLine($"Encrypted XML not found: {encryptedXmlPath}");
            return;
        }

        byte[] encryptedData = File.ReadAllBytes(encryptedXmlPath);
        string decryptedXml;

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV  = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream ms = new MemoryStream(encryptedData))
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs))
            {
                decryptedXml = sr.ReadToEnd();
            }
        }

        // -----------------------------------------------------------------
        // 2. Load the PDF template and import the form data from XML
        // -----------------------------------------------------------------
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }

        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            // Populate the PDF form fields from the decrypted XML.
            PopulateFormFromXml(pdfDoc, decryptedXml);

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Parses a simple XML representation of form data and fills the PDF form.
    /// Expected XML format:
    /// <formdata>
    ///   <field name="FieldName1">Value1</field>
    ///   <field name="FieldName2">Value2</field>
    ///   ...
    /// </formdata>
    /// </summary>
    private static void PopulateFormFromXml(Document doc, string xmlContent)
    {
        if (string.IsNullOrWhiteSpace(xmlContent))
            return;

        XDocument xDoc;
        try
        {
            xDoc = XDocument.Parse(xmlContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse XML form data: {ex.Message}");
            return;
        }

        var form = doc.Form;
        if (form == null)
            return;

        // Iterate over each <field> element in the XML and set the matching PDF field.
        foreach (var fieldElement in xDoc.Descendants("field"))
        {
            var nameAttr = fieldElement.Attribute("name");
            if (nameAttr == null)
                continue; // skip malformed entries

            string fieldName = nameAttr.Value;
            string fieldValue = fieldElement.Value ?? string.Empty;

            // Retrieve the field from the PDF form. The indexer returns a Field object.
            Field pdfField = form[fieldName] as Field;
            if (pdfField != null)
            {
                pdfField.Value = fieldValue;
            }
            else
            {
                // If the direct indexer lookup fails, fall back to searching the Fields collection.
                foreach (Field f in form.Fields)
                {
                    // PartialName is the name used in the PDF form definition.
                    if (string.Equals(f.PartialName, fieldName, StringComparison.OrdinalIgnoreCase))
                    {
                        f.Value = fieldValue;
                        break;
                    }
                }
            }
        }
    }
}
