using System;
using System.IO;
using Aspose.Pdf; // Core API

class Program
{
    static void Main()
    {
        const string inputPdf   = "protected.pdf";   // Existing password‑protected PDF
        const string outputPdf  = "protected_with_attachment.pdf";
        const string password   = "user123";         // Correct user password
        const string attachFile = "attachment.txt";  // File to embed as attachment

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachFile}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the user password.
            using (Document doc = new Document(inputPdf, password))
            {
                // Create a file specification for the attachment.
                FileSpecification fileSpec = new FileSpecification(attachFile);

                // Add the file specification to the EmbeddedFiles collection.
                doc.EmbeddedFiles.Add(fileSpec);

                // Save the modified PDF (still encrypted with the same password).
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
