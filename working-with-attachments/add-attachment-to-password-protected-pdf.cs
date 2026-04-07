using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "protected.pdf";   // Existing password‑protected PDF
        const string userPwd    = "user123";        // Password that opens the document
        const string attachment = "extra.docx";     // File to attach
        const string outputPdf  = "protected_with_attachment.pdf";

        // Verify that the source PDF and attachment exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the user password.
            using (Document doc = new Document(inputPdf, userPwd))
            {
                // Create a FileSpecification for the file to embed.
                // The first argument is the name that will appear in the attachment list.
                var fileSpec = new FileSpecification(Path.GetFileName(attachment), "Embedded attachment");
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachment));
                fileSpec.Description = "Additional document attached via Aspose.Pdf";

                // Add the file specification to the document's embedded files collection.
                doc.EmbeddedFiles.Add(fileSpec);

                // Save the modified PDF. The document remains encrypted with the same passwords.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Incorrect password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
