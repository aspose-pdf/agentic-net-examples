using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "secured.pdf";
        const string userPassword = "user123";
        const string attachmentPath = "attachment.txt";
        const string outputPath = "secured_with_attachment.pdf";

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Create a FileSpecification for the attachment and load its contents into a stream
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
                {
                    // The file data must be supplied via a stream for embedding
                    Contents = new MemoryStream(File.ReadAllBytes(attachmentPath)),
                    // Optional description shown in PDF viewers
                    Description = "Embedded attachment"
                };

                // Add the attachment to the PDF's EmbeddedFiles collection
                doc.EmbeddedFiles.Add(fileSpec);

                // Save the modified PDF (encryption is preserved automatically)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Attachment added and saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
