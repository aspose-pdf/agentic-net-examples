using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    // Encrypts a byte array with AES using a password‑derived key.
    static byte[] EncryptBytes(byte[] plainData, string password)
    {
        // Salt can be static for demo purposes; in production use a random salt and store it.
        byte[] salt = new byte[] { 0x21, 0x4A, 0x3C, 0x5F, 0x77, 0x88, 0x99, 0xAA };
        const int iterations = 10000;
        using (Aes aes = Aes.Create())
        {
            // Derive key and IV using PBKDF2 (static method) to avoid obsolete constructor warning.
            int keyBytes = aes.KeySize / 8;
            int ivBytes = aes.BlockSize / 8;
            byte[] key = Rfc2898DeriveBytes.Pbkdf2(password, salt, keyBytes, HashAlgorithmName.SHA256, iterations);
            byte[] iv = Rfc2898DeriveBytes.Pbkdf2(password, salt, ivBytes, HashAlgorithmName.SHA256, iterations);
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream crypto = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                crypto.Write(plainData, 0, plainData.Length);
                crypto.FlushFinalBlock();
                return ms.ToArray();
            }
        }
    }

    static void Main()
    {
        const string outputPdfPath   = "SignedWithEncryptedAttachments.pdf";
        const string attachmentPath  = "secret.txt";          // file to be attached
        const string attachmentPwd   = "attachPass123";      // password for attachment encryption
        const string certPath        = "myCertificate.pfx"; // path to signing certificate
        const string certPassword    = "certPass";           // certificate password

        // Ensure the attachment file exists.
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Create a new PDF document.
        using (Document pdfDoc = new Document())
        {
            // Add a simple page with some text (optional, just to have content).
            Page page = pdfDoc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("PDF with encrypted attachment and digital signature."));

            // Read the attachment, encrypt it, and add to the PDF as an embedded file.
            byte[] plainBytes = File.ReadAllBytes(attachmentPath);
            byte[] encryptedBytes = EncryptBytes(plainBytes, attachmentPwd);

            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath) + ".enc", "Encrypted attachment");
            fileSpec.Contents = new MemoryStream(encryptedBytes);
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // ------------------------------------------------------------
            // Create a signature field and digitally sign the document.
            // ------------------------------------------------------------
            // Define the rectangle where the visual signature will appear.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            // Create the signature field.
            SignatureField sigField = new SignatureField(pdfDoc, sigRect);
            // Add the field to the document's form collection.
            pdfDoc.Form.Add(sigField);

            // Initialise PKCS#1 signature handler with the certificate.
            PKCS1 pkcs = new PKCS1(certPath, certPassword);
            // Sign the field.
            sigField.Sign(pkcs);

            // Save the signed PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF created, attachment encrypted, and signed: {outputPdfPath}");
    }
}
