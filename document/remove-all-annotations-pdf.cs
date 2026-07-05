using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization; // HiddenDataSanitizer and options reside here

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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Configure the hidden‑data sanitizer to remove all annotations
            var options = new HiddenDataSanitizationOptions
            {
                RemoveAnnotations = true // remove every annotation from the document
            };

            // Apply the sanitization (lifecycle rule: use the provided sanitizer class)
            var sanitizer = new HiddenDataSanitizer(options);
            sanitizer.Sanitize(doc);

            // Save the sanitized PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
