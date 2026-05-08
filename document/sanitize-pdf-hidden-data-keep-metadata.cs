using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, configure hidden‑data sanitization, and save.
        using (Document doc = new Document(inputPath))
        {
            // Configure sanitization options: keep metadata, remove all other hidden data.
            var options = new HiddenDataSanitizationOptions
            {
                RemoveMetadata = false // keep document metadata (title, author, etc.)
            };

            // Create the sanitizer and apply it to the document.
            var sanitizer = new HiddenDataSanitizer(options);
            sanitizer.Sanitize(doc);

            // Save the sanitized PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
