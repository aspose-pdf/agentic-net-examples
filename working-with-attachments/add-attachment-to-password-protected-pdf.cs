using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf      = "protected.pdf";      // encrypted source
        const string userPassword  = "user123";            // password to open
        const string attachment    = "attach.txt";         // file to embed
        const string outputPdf     = "protected_with_attachment.pdf";

        // Validate files exist
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
            // Open the encrypted PDF using the correct password
            using (Document doc = new Document(inputPdf, userPassword))
            {
                // Create a file specification for the attachment (core API)
                FileSpecification fileSpec = new FileSpecification(attachment)
                {
                    Description = "Sample attachment",
                    // The name that will appear in the PDF attachment list
                    Name = Path.GetFileName(attachment)
                };

                // Add the embedded file to the document
                doc.EmbeddedFiles.Add(fileSpec);

                // Save the modified PDF (encryption is preserved)
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
