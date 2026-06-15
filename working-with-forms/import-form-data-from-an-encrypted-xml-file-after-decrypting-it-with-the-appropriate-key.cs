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
        const string pdfPath = "input.pdf";
        const string encryptedXmlPath = "data.xml.enc";
        const string outputPdfPath = "output.pdf";
        const string password = "mySecretKey"; // replace with your actual key

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(encryptedXmlPath))
        {
            Console.Error.WriteLine($"Encrypted XML file not found: {encryptedXmlPath}");
            return;
        }

        try
        {
            // Decrypt the XML file (expects IV + ciphertext)
            byte[] decryptedXml = DecryptFile(encryptedXmlPath, password);

            // Load decrypted XML into an XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream(decryptedXml))
            {
                xmlDoc.Load(ms);
            }

            // Open the PDF and assign the XFA data from the XML
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfPath))
            {
                Aspose.Pdf.Forms.Form form = pdfDoc.Form;
                form.AssignXfa(xmlDoc);
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported successfully to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Decrypts a file encrypted with AES-CBC. The first 16 bytes are the IV.
    static byte[] DecryptFile(string filePath, string password)
    {
        byte[] fileBytes = File.ReadAllBytes(filePath);
        byte[] iv = new byte[16];
        Array.Copy(fileBytes, 0, iv, 0, iv.Length);
        byte[] cipherText = new byte[fileBytes.Length - iv.Length];
        Array.Copy(fileBytes, iv.Length, cipherText, 0, cipherText.Length);

        // Derive a 256‑bit key from the password using SHA‑256
        using (SHA256 sha = SHA256.Create())
        {
            byte[] key = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream msDecrypt = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(msDecrypt, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherText, 0, cipherText.Length);
                    cs.FlushFinalBlock();
                    return msDecrypt.ToArray();
                }
            }
        }
    }
}