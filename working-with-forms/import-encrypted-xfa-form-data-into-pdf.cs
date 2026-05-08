using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";               // PDF with an interactive form
        const string encryptedXmlPath = "data.xml.enc";   // Encrypted XML containing form data (XFA)
        const string outputPdfPath = "filled_form.pdf";  // Resulting PDF

        // -----------------------------------------------------------------
        // Verify that the encrypted XML file exists before attempting decryption.
        // -----------------------------------------------------------------
        if (!File.Exists(encryptedXmlPath))
        {
            Console.WriteLine($"Error: Encrypted XML file '{encryptedXmlPath}' was not found.");
            return;
        }

        // -----------------------------------------------------------------
        // Decrypt the XML file (AES example). Replace key/IV with your own.
        // -----------------------------------------------------------------
        byte[] key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF"); // 32‑byte key for AES‑256
        byte[] iv  = Encoding.UTF8.GetBytes("0123456789ABCDEF");                 // 16‑byte IV

        XmlDocument xmlDoc = new XmlDocument();

        try
        {
            using (FileStream encStream = File.OpenRead(encryptedXmlPath))
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;

                using (CryptoStream cryptoStream = new CryptoStream(encStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    // Load the decrypted XML directly into an XmlDocument
                    xmlDoc.Load(cryptoStream);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Decryption failed: {ex.Message}");
            return;
        }

        // ---------------------------------------------------------------
        // Load the target PDF, assign the decrypted XFA data, and save.
        // ---------------------------------------------------------------
        try
        {
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Assign the XFA data (XML) to the PDF form.
                pdfDoc.Form.AssignXfa(xmlDoc);

                // Save the updated PDF.
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"PDF processing failed: {ex.Message}");
        }
    }
}
