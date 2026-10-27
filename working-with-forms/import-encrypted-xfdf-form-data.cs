using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "template.pdf";
        const string encryptedXmlPath = "data.xml.enc";
        const string outputPdfPath = "filled.pdf";

        // Replace with your actual key (32 bytes for AES‑256) and IV (16 bytes)
        // For demo purposes we pad/truncate the strings to required lengths.
        byte[] key = GetFixedLengthBytes("YOUR_32_BYTE_KEY_GOES_HERE_____", 32);
        byte[] iv  = GetFixedLengthBytes("YOUR_16_BYTE_IV__", 16);

        // Verify that the source files exist before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF template not found: {Path.GetFullPath(pdfPath)}");
            return;
        }
        if (!File.Exists(encryptedXmlPath))
        {
            Console.WriteLine($"Encrypted XML file not found: {Path.GetFullPath(encryptedXmlPath)}");
            return;
        }

        // Decrypt the XML file
        string xmlContent = DecryptXmlFile(encryptedXmlPath, key, iv);
        if (string.IsNullOrWhiteSpace(xmlContent))
        {
            Console.WriteLine("Decrypted XML content is empty.");
            return;
        }

        // Load XML into an XDocument
        XDocument xDoc = XDocument.Parse(xmlContent);

        // Open the PDF template inside a using block (ensures disposal)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Expect XML like: <FormData><Field name="FieldName">Value</Field>...</FormData>
            foreach (XElement fieldElem in xDoc.Descendants("Field"))
            {
                XAttribute nameAttr = fieldElem.Attribute("name");
                if (nameAttr == null) continue;

                string fieldName = nameAttr.Value;
                string fieldValue = fieldElem.Value;

                // Retrieve the form field; the indexer returns a Field object or null.
                // Use a nullable variable to avoid CS8600 warning.
                Field? pdfField = pdfDoc.Form[fieldName] as Field;
                if (pdfField != null)
                {
                    pdfField.Value = fieldValue;
                }
                else
                {
                    Console.WriteLine($"Warning: No form field named '{fieldName}' found in the PDF template.");
                }
            }

            // Save the filled PDF (PDF format, no SaveOptions needed)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }

    // Decrypts an AES‑CBC encrypted file and returns the plaintext as a string
    static string DecryptXmlFile(string encryptedPath, byte[] key, byte[] iv)
    {
        using (FileStream fs = new FileStream(encryptedPath, FileMode.Open, FileAccess.Read))
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (CryptoStream cs = new CryptoStream(fs, aes.CreateDecryptor(), CryptoStreamMode.Read))
            using (MemoryStream ms = new MemoryStream())
            {
                cs.CopyTo(ms);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }

    // Helper: ensures a byte array of the exact required length (pads with zeros or truncates).
    static byte[] GetFixedLengthBytes(string input, int length)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(input);
        if (bytes.Length == length) return bytes;
        byte[] fixedBytes = new byte[length];
        Array.Copy(bytes, 0, fixedBytes, 0, Math.Min(bytes.Length, length));
        return fixedBytes;
    }
}
