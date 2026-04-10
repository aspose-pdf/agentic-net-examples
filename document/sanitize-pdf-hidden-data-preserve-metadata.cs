using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure sanitization options:
            // - Keep metadata (no option removes metadata)
            // - Remove JavaScript and actions
            // - Remove search index and private info
            // - Remove annotations
            // - Flatten forms and layers to eliminate hidden form data
            var options = new HiddenDataSanitizationOptions
            {
                RemoveJavaScriptsAndActions   = true,
                RemoveSearchIndexAndPrivateInfo = true,
                RemoveAnnotations             = true,
                FlattenForms                  = true,
                FlattenLayers                 = true
                // Other options can be set as needed
            };

            // Create the sanitizer and apply it to the document
            var sanitizer = new HiddenDataSanitizer(options);
            sanitizer.Sanitize(doc);

            // Save the sanitized PDF (metadata is preserved)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}