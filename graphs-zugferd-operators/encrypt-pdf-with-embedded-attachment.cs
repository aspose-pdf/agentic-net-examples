using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // source PDF
        const string outputPdf = "output_encrypted.pdf";   // result PDF
        const string attachmentPath = "secret.txt";        // file to attach
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a file specification for the attachment using the constructor that accepts the file path and description
            FileSpecification fileSpec = new FileSpecification(attachmentPath, "Secret attachment");
            // Add the file specification to the document's EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Define permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the whole document (including the attachment) with AES‑256
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Encrypted PDF with attachment saved to '{outputPdf}'.");
    }
}
