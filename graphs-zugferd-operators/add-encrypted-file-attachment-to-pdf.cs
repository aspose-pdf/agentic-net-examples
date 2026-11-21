using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPdf       = "input.pdf";
        const string attachmentPath = "secret.txt";
        const string outputPdf      = "output_encrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

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

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Create a FileSpecification for the attachment
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
                {
                    Description = "Confidential data",
                    // MimeType property is not part of FileSpecification; it can be stored in the description if needed
                };
                // Set the file contents via a memory stream
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));

                // Add the file specification to the EmbeddedFiles collection
                doc.EmbeddedFiles.Add(fileSpec);

                // Define permissions (adjust as needed). Using a flag that exists across supported versions.
                Permissions perms = Permissions.PrintDocument;

                // Encrypt the entire PDF (including the embedded file) with AES‑256
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Encrypted PDF with attachment saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
