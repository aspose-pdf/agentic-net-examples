using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "protected.pdf";
        const string outputPdfPath = "protected_with_attachment.pdf";
        const string pdfPassword = "user123";          // user password for the protected PDF
        const string attachmentPath = "attachment.txt"; // file to attach

        // Verify required files exist
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

        try
        {
            // Open the password‑protected PDF. The password is supplied in the constructor.
            using (Document doc = new Document(inputPdfPath, pdfPassword))
            {
                // Create a FileSpecification for the attachment.
                // Use the overload that takes only the file name, then assign the file data via the Contents stream.
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
                {
                    Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
                };

                // Add the attachment to the PDF via the EmbeddedFiles collection.
                doc.EmbeddedFiles.Add(fileSpec);

                // Save the modified PDF.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");
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
