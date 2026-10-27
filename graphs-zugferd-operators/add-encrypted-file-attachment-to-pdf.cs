using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string attachmentPath = "secret.txt";
        const string outputPdfPath = "output_encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the existing PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Embed the external file using the EmbeddedFiles collection
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath));
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
            // Optional: set MIME type for the embedded file
            fileSpec.MIMEType = "application/octet-stream";
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Define permissions for the encrypted PDF (e.g., allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the entire PDF (including the embedded file) with AES‑256
            pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Encrypted PDF with attachment saved to '{outputPdfPath}'.");
    }
}
