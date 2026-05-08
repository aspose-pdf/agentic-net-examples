using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_sanitized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure sanitization options to remove JavaScript and related actions
            var options = new HiddenDataSanitizationOptions
            {
                RemoveJavaScriptsAndActions = true,
                RemoveSearchIndexAndPrivateInfo = true,
                RemoveAnnotations = true,
                FlattenForms = true,
                FlattenLayers = true
            };

            // Create the sanitizer and apply it to the document
            var sanitizer = new HiddenDataSanitizer(options);
            sanitizer.Sanitize(doc);

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}