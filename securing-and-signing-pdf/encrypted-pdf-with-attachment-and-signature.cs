using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "encrypted_signed.pdf";
        const string attachmentPath = "sample.txt";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "pfxPass";

        // Ensure the attachment file exists
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Ensure the signing certificate exists
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required for placing a signature field)
            Page page = doc.Pages.Add();

            // Embed a file as an attachment using FileSpecification
            using (FileStream attachStream = File.OpenRead(attachmentPath))
            {
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath), "Embedded attachment");
                fileSpec.Contents = attachStream; // set the file data
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Encrypt the entire document (including attachments) with a user/owner password
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Create a signature field on the first page
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(page, sigRect);
            doc.Form.Add(sigField, 1);

            // Prepare the digital signature using PKCS#1 (or PKCS7) with the provided certificate
            PKCS1 pkcs1 = new PKCS1(pfxPath, pfxPassword);

            // Sign the document using the signature field
            sigField.Sign(pkcs1);

            // Save the final PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created, encrypted, and digitally signed: {outputPath}");
    }
}
