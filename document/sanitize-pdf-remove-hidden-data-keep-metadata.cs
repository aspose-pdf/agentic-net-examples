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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure the sanitizer: keep document metadata, remove all other hidden data
            var options = new HiddenDataSanitizationOptions();
            // Remove JavaScript actions (metadata is kept by default)
            options.RemoveJavaScriptsAndActions = true;

            // Delete any embedded files (they are considered hidden data)
            doc.EmbeddedFiles.Delete();

            // Apply the sanitization using the HiddenDataSanitizer
            var sanitizer = new HiddenDataSanitizer(options);
            sanitizer.Sanitize(doc);

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
