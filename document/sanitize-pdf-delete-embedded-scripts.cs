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
            HiddenDataSanitizationOptions options = new HiddenDataSanitizationOptions();
            options.RemoveJavaScriptsAndActions = true;

            // Remove all embedded file attachments before sanitization
            doc.EmbeddedFiles.Delete();

            HiddenDataSanitizer sanitizer = new HiddenDataSanitizer(options);
            sanitizer.Sanitize(doc);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}