using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF to which the attachment will be added
        const string inputPdfPath = "input.pdf";
        // File that will be attached (its content will be encrypted together with the PDF)
        const string attachmentFilePath = "secret.txt";
        // Output encrypted PDF
        const string outputPdfPath = "output_encrypted.pdf";

        // Passwords for PDF encryption
        const string userPassword = "user123";   // password required to open the PDF
        const string ownerPassword = "owner123"; // password with full permissions

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Load the existing PDF (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 1. Embed the file as an embedded file (portfolio entry).
            // ------------------------------------------------------------
            // Create a FileSpecification with a display name.
            FileSpecification fileSpec = new FileSpecification(Path.GetFileName(attachmentFilePath));
            // Set the file contents – this stream will be encrypted together with the PDF.
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));
            // Optionally set a description.
            fileSpec.Description = "Encrypted attachment";
            // Add the specification to the document's EmbeddedFiles collection.
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // ------------------------------------------------------------
            // 2. Define permissions for the encrypted PDF.
            // ------------------------------------------------------------
            Permissions permissions = Permissions.PrintDocument | Permissions.ModifyContent;

            // ------------------------------------------------------------
            // 3. Encrypt the whole document (including the embedded file).
            // ------------------------------------------------------------
            pdfDoc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // ------------------------------------------------------------
            // 4. Save the encrypted PDF.
            // ------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Encrypted PDF with attachment saved to '{outputPdfPath}'.");
    }
}
