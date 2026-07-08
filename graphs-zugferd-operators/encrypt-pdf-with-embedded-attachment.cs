using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_encrypted.pdf";
        const string attachmentPath = "secret.txt";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPdf) || !File.Exists(attachmentPath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Read attachment data
            byte[] attachmentData = File.ReadAllBytes(attachmentPath);

            // Create a FileSpecification for the embedded file
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath), "Encrypted attachment");
            // Assign the file contents via a memory stream
            fileSpec.Contents = new MemoryStream(attachmentData);

            // Add the embedded file to the PDF
            doc.EmbeddedFiles.Add(fileSpec);

            // Define permissions (allow printing, extracting, and modifying content)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent | Permissions.ModifyContent;

            // Encrypt the document (including the embedded file) with user/owner passwords using AES‑256
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF (attachment content is encrypted as part of the document)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Encrypted PDF with attachment saved to '{outputPdf}'.");
    }
}
