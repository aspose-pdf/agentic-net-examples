using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "encrypted_signed.pdf";
        const string attachmentPath = "attachment.txt";   // file to embed
        const string pfxPath = "certificate.pfx";        // signing certificate
        const string pfxPassword = "pfxPassword";        // certificate password
        const string userPassword = "user123";           // PDF user password
        const string ownerPassword = "owner123";         // PDF owner password

        // Ensure the source files exist
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
            // Add a blank page (required for the signature field)
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Embed an attachment into the PDF (document‑level attachment)
            // -------------------------------------------------
            // Use the constructor that accepts the file path and a description.
            FileSpecification fileSpec = new FileSpecification(attachmentPath, Path.GetFileName(attachmentPath));
            doc.EmbeddedFiles.Add(fileSpec);

            // -------------------------------------------------
            // 2. Add a signature field to the document
            // -------------------------------------------------
            // Define the rectangle where the signature appearance will be placed
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            // Create the signature field and add it to the form collection
            SignatureField signatureField = new SignatureField(page, sigRect);
            doc.Form.Add(signatureField);

            // -------------------------------------------------
            // 3. Sign the document using a PKCS#7 detached signature
            // -------------------------------------------------
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                // PKCS7Detached(Stream pfx, string password) creates a detached signature
                PKCS7Detached pkcs7Signature = new PKCS7Detached(pfxStream, pfxPassword);
                // Sign the field
                signatureField.Sign(pkcs7Signature);
            }

            // -------------------------------------------------
            // 4. Encrypt the entire PDF (including attachments and signature)
            // -------------------------------------------------
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // -------------------------------------------------
            // 5. Save the final PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created, encrypted and signed: {outputPath}");
    }
}
