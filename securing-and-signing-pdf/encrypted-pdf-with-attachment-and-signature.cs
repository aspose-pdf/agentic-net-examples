using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        // Input files
        const string attachmentPath = "attachment.txt";   // file to embed
        const string pfxPath        = "certificate.pfx"; // signing certificate
        const string pfxPassword    = "pfxPassword";

        // Output PDF
        const string outputPdf = "encrypted_signed.pdf";

        // Passwords for PDF encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Ensure required files exist
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate not found: {pfxPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Add an embedded file (attachment) to the PDF
            // -------------------------------------------------
            // Use FileSpecification – the type stored in Document.EmbeddedFiles
            var fileSpec = new FileSpecification(attachmentPath)
            {
                Description = "Sample attachment"
            };
            doc.EmbeddedFiles.Add(fileSpec);

            // -------------------------------------------------
            // 2. Create a signature field on the first page
            // -------------------------------------------------
            var sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            var signatureField = new SignatureField(page, sigRect);
            doc.Form.Add(signatureField);

            // -------------------------------------------------
            // 3. Sign the document using a PKCS#7 detached signature
            // -------------------------------------------------
            var pkcs7Signature = new PKCS7Detached(pfxPath, pfxPassword);
            signatureField.Sign(pkcs7Signature);

            // -------------------------------------------------
            // 4. Encrypt the PDF with user/owner passwords
            // -------------------------------------------------
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // -------------------------------------------------
            // 5. Save the final PDF
            // -------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created, encrypted and signed: {outputPdf}");
    }
}
