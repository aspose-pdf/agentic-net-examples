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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure sanitization options:
            // - Remove hidden annotations, search index, private info, forms, layers, JavaScript.
            // - Keep metadata (no option removes metadata, so we leave it untouched).
            HiddenDataSanitizationOptions options = new HiddenDataSanitizationOptions
            {
                RemoveAnnotations = true,
                RemoveSearchIndexAndPrivateInfo = true,
                FlattenForms = true,
                FlattenLayers = true,
                RemoveJavaScriptsAndActions = true,
                ConvertPagesToImages = false // keep visible content as is
                // ImageCompressionOptions and ImageDpi can remain default
            };

            // Create the sanitizer with the configured options
            HiddenDataSanitizer sanitizer = new HiddenDataSanitizer(options);

            // Apply sanitization to the document
            sanitizer.Sanitize(doc);

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}