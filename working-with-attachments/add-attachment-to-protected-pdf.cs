using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "protected.pdf";          // Encrypted source PDF
        const string userPassword = "user123";            // Password to open the PDF
        const string attachmentFile = "attachment.txt";   // File to attach
        const string outputPdf = "protected_with_attachment.pdf";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the correct password
            using (Document doc = new Document(inputPdf, userPassword))
            {
                // Embed the attachment into the PDF
                using (FileStream attachmentStream = File.OpenRead(attachmentFile))
                {
                    // Create a FileSpecification – the first argument is the file name that will appear in the PDF,
                    // the second argument is a description (can be the same value).
                    var fileSpec = new FileSpecification(Path.GetFileName(attachmentFile), Path.GetFileName(attachmentFile));
                    fileSpec.Contents = attachmentStream; // Set the file data
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Save the modified PDF (encryption remains intact)
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
