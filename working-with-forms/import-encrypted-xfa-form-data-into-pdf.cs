using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // Prepare sample files (input PDF and encrypted XML) for the sandbox
        // -----------------------------------------------------------------
        const string pdfPath = "input.pdf";
        const string encryptedXmlPath = "data.xml.enc";
        const string outputPdfPath = "output.pdf";

        // Sample AES key (32 bytes) and IV (16 bytes). In a real scenario replace
        // these with your actual key/IV values.
        byte[] aesKey = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF"); // 32 bytes
        byte[] aesIv  = Encoding.UTF8.GetBytes("0123456789ABCDEF");               // 16 bytes

        // -----------------------------------------------------------------
        // 1. Create a minimal PDF that contains a form (required for XFA import)
        // -----------------------------------------------------------------
        using (Document seedPdf = new Document())
        {
            // Add a page so the document is not empty
            seedPdf.Pages.Add();

            // Add a simple text box field – the presence of a form is enough
            // Correct constructor: TextBoxField(Page, Rectangle)
            TextBoxField txt = new TextBoxField(seedPdf.Pages[1], new Rectangle(100, 600, 300, 650))
            {
                PartialName = "SampleField",
                Value = ""
            };
            seedPdf.Form.Add(txt);

            seedPdf.Save(pdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Create a tiny XFA XML document and encrypt it to "data.xml.enc"
        // -----------------------------------------------------------------
        string xfaXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                        "<xfa:datasets xmlns:xfa=\"http://www.xfa.org/schema/xfa-data/1.0/\"></xfa:datasets>";
        byte[] plainBytes = Encoding.UTF8.GetBytes(xfaXml);

        using (Aes aes = Aes.Create())
        {
            aes.Key = aesKey;
            aes.IV = aesIv;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;

            using (FileStream encStream = new FileStream(encryptedXmlPath, FileMode.Create, FileAccess.Write))
            using (CryptoStream cryptoStream = new CryptoStream(encStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
            }
        }

        // -----------------------------------------------------------------
        // 3. Decrypt the XML file back into an XmlDocument
        // -----------------------------------------------------------------
        XmlDocument xmlDoc = new XmlDocument();
        using (FileStream encryptedStream = File.OpenRead(encryptedXmlPath))
        using (Aes aes = Aes.Create())
        {
            aes.Key = aesKey;
            aes.IV = aesIv;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;

            using (CryptoStream cryptoStream = new CryptoStream(encryptedStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
            using (MemoryStream decryptedStream = new MemoryStream())
            {
                cryptoStream.CopyTo(decryptedStream);
                decryptedStream.Position = 0;
                xmlDoc.Load(decryptedStream);
            }
        }

        // -----------------------------------------------------------------
        // 4. Load the PDF and assign the decrypted XFA data
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            pdfDoc.Form.AssignXfa(xmlDoc);
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}
