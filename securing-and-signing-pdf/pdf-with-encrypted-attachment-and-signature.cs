using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string secretFilePath = "secret.txt";          // file to be encrypted and attached
        const string certificatePath = "cert.pfx";          // signing certificate
        const string certificatePassword = "certPassword";  // certificate password
        const string outputPdfPath = "SignedWithEncryptedAttachment.pdf";

        // Verify required files exist
        if (!File.Exists(secretFilePath))
        {
            Console.Error.WriteLine($"Secret file not found: {secretFilePath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // -------------------------------------------------
        // 1. Encrypt the attachment using AES-256 (CBC mode)
        // -------------------------------------------------
        byte[] encryptedAttachment;
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.GenerateKey();
            aes.GenerateIV();

            using (FileStream input = File.OpenRead(secretFilePath))
            using (MemoryStream encrypted = new MemoryStream())
            using (CryptoStream crypto = new CryptoStream(encrypted, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                input.CopyTo(crypto);
                crypto.FlushFinalBlock();
                encryptedAttachment = encrypted.ToArray();
            }
        }

        // -------------------------------------------------
        // 2. Create a new PDF document and add a simple page
        // -------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            // Add a page with a title
            Page page = pdfDoc.Pages.Add();
            TextFragment title = new TextFragment("PDF with Encrypted Attachment and Digital Signature")
            {
                TextState = { FontSize = 14, FontStyle = FontStyles.Bold }
            };
            title.Position = new Position(50, 750);
            page.Paragraphs.Add(title);

            // -------------------------------------------------
            // 3. Attach the encrypted file to the PDF (embedded file)
            // -------------------------------------------------
            string attachmentName = Path.GetFileName(secretFilePath) + ".enc";
            var fileSpec = new FileSpecification(attachmentName);
            fileSpec.Contents = new MemoryStream(encryptedAttachment);
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // -------------------------------------------------
            // 4. Add a visible signature field and sign the document
            // -------------------------------------------------
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                PartialName = "Signature1"
            };
            pdfDoc.Form.Add(sigField, page.Number);

            PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword)
            {
                Reason = "Document signed with encrypted attachment",
                Location = "My Company",
                ContactInfo = "contact@mycompany.com"
            };

            sigField.Sign(pkcs7);

            // -------------------------------------------------
            // 5. Save the signed PDF
            // -------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF created, encrypted attachment added, and signed successfully: {outputPdfPath}");
    }
}
