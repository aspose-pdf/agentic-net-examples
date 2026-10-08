using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization; // Correct namespace for hidden‑data sanitization

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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Configure hidden‑data sanitization options
                var options = new HiddenDataSanitizationOptions
                {
                    // Enable removal of search index and private information
                    RemoveSearchIndexAndPrivateInfo = true
                };

                // Create the sanitizer with the configured options and apply it
                var sanitizer = new HiddenDataSanitizer(options);
                sanitizer.Sanitize(doc);

                // Save the sanitized PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
