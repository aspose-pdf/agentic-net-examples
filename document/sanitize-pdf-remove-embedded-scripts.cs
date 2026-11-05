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

        using (Document doc = new Document(inputPath))
        {
            // Remove any embedded file attachments first (optional but often required)
            doc.EmbeddedFiles?.Delete();

            // Configure hidden‑data sanitization: delete all embedded JavaScript actions
            var options = new HiddenDataSanitizationOptions
            {
                RemoveJavaScriptsAndActions = true
            };

            // Apply the sanitization to the document
            var sanitizer = new HiddenDataSanitizer(options);
            sanitizer.Sanitize(doc);

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
