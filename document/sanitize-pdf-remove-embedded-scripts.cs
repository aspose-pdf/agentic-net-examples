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

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Configure the hidden‑data sanitizer to remove JavaScript actions.
            var options = new HiddenDataSanitizationOptions();
            options.RemoveJavaScriptsAndActions = true;

            // Delete embedded files (including embedded scripts) before sanitization.
            doc.EmbeddedFiles?.Delete();

            // Apply sanitization with the configured options.
            var sanitizer = new HiddenDataSanitizer(options);
            sanitizer.Sanitize(doc);

            // Save the sanitized PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
