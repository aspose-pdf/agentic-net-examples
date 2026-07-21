using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization; // Updated namespace for hidden data sanitization

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
            // Configure hidden data sanitization options to delete embedded scripts
            var options = new HiddenDataSanitizationOptions();
            options.RemoveJavaScriptsAndActions = true; // delete embedded JavaScript and actions

            // Optionally delete embedded files (if any) before sanitization
            doc.EmbeddedFiles.Delete();

            // Create the sanitizer with the configured options and apply it
            var sanitizer = new HiddenDataSanitizer(options);
            sanitizer.Sanitize(doc);

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
