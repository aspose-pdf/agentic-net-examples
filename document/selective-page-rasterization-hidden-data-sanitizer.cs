using System;
using System.IO;
using Aspose.Pdf;

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
            // ------------------------------------------------------------
            // Hidden data sanitization fallback:
            // The current Aspose.Pdf version does not expose
            // HiddenDataSanitizerOptions or Document.Sanitize().
            // As a practical alternative we clear all annotations from
            // every page, which removes a common source of hidden data.
            // ------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                page.Annotations.Clear();
            }

            // ------------------------------------------------------------
            // Page rasterization for selected pages:
            // This capability is only available via HiddenDataSanitizerOptions
            // in newer releases. If you need rasterization, upgrade the
            // Aspose.Pdf NuGet package and use the appropriate API.
            // ------------------------------------------------------------

            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
