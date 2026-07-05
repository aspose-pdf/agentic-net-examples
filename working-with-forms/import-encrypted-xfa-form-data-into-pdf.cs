using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Aspose.Pdf;

class Program
{
    // Example symmetric key and IV for AES decryption (must match the encryption used)
    // AES-256 requires a 32‑byte key; IV is 16 bytes.
    private static readonly byte[] _key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF");
    private static readonly byte[] _iv  = Encoding.UTF8.GetBytes("ABCDEF0123456789");

    static void Main()
    {
        // Paths for the PDF, the encrypted XML containing form data, and the output PDF
        const string pdfPath = "form.pdf";
        const string encryptedXmlPath = "data.xml.enc";
        const string outputPdfPath = "form_filled.pdf";

        // ------------------------------------------------------------
        // Ensure an encrypted XML file exists – create a sample one if missing.
        // ------------------------------------------------------------
        if (!File.Exists(encryptedXmlPath))
        {
            CreateSampleEncryptedXml(encryptedXmlPath);
            Console.WriteLine($"Sample encrypted XML created at '{encryptedXmlPath}'.");
        }

        // ------------------------------------------------------------
        // Decrypt the XML file into an XmlDocument
        // ------------------------------------------------------------
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            using (FileStream encryptedStream = File.OpenRead(encryptedXmlPath))
            using (MemoryStream decryptedStream = new MemoryStream())
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = _key;
                    aes.IV  = _iv;

                    using (CryptoStream crypto = new CryptoStream(encryptedStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        crypto.CopyTo(decryptedStream);
                    }
                }
                decryptedStream.Position = 0;
                xmlDoc.Load(decryptedStream);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during XML decryption/loading: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Load the target PDF (create a blank one if it does not exist)
        // ------------------------------------------------------------
        Document pdfDoc;
        if (File.Exists(pdfPath))
        {
            pdfDoc = new Document(pdfPath);
        }
        else
        {
            pdfDoc = new Document();
            pdfDoc.Pages.Add(); // add a blank page so the document is not empty
            Console.WriteLine($"PDF '{pdfPath}' not found – a new blank PDF will be used.");
        }

        // Assign the decrypted XFA data to the PDF form (if the PDF actually contains a form)
        try
        {
            pdfDoc.Form.AssignXfa(xmlDoc);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AssignXfa failed: {ex.Message}");
            // Continue – the PDF may not have an XFA form.
        }

        // Save the result
        pdfDoc.Save(outputPdfPath);
        Console.WriteLine($"Form data imported and PDF saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Creates a tiny XML document, encrypts it with AES‑256 using the predefined key/IV,
    /// and writes the ciphertext to the specified file path.
    /// </summary>
    private static void CreateSampleEncryptedXml(string outputPath)
    {
        // Simple XML that mimics XFA form data
        string sampleXml = "<?xml version='1.0' encoding='UTF-8'?><xfa:datasets xmlns:xfa='http://www.xfa.org/schema/xfa-data/1.0/'><xfa:data><field1>Value1</field1><field2>Value2</field2></xfa:data></xfa:datasets>";
        byte[] plainBytes = Encoding.UTF8.GetBytes(sampleXml);

        using (Aes aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV  = _iv;
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            using (CryptoStream crypto = new CryptoStream(outStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                crypto.Write(plainBytes, 0, plainBytes.Length);
            }
        }
    }
}
