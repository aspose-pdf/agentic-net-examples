using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Configure hidden‑data sanitization to remove all annotations
            var options = new HiddenDataSanitizationOptions();
            options.RemoveAnnotations = true;

            var sanitizer = new HiddenDataSanitizer(options);
            sanitizer.Sanitize(doc);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations removed and saved to '{outputPath}'.");
    }
}