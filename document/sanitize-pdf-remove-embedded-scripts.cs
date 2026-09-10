using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure hidden data sanitization to remove JavaScript/actions
            var options = new HiddenDataSanitizationOptions
            {
                RemoveJavaScriptsAndActions = true
            };

            // Delete any embedded files (e.g., attachments) before sanitizing
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                doc.EmbeddedFiles.Delete();
            }

            // Create the sanitizer with the configured options and run it
            var sanitizer = new HiddenDataSanitizer(options);
            sanitizer.Sanitize(doc);

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}