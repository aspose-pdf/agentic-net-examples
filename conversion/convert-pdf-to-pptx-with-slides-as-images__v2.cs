using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "presentation.pdf";
        const string outputPath = "presentation_full.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure PPTX save options: render each slide as an image
            var pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true
            };

            // Attach a custom progress handler (UnifiedSaveOptions.CustomProgressHandler)
            pptxOptions.CustomProgressHandler = info =>
            {
                // Simple console logging of progress events
                Console.WriteLine($"{DateTime.Now:T} - {info.EventType}: {info.Value}/{info.MaxValue}");
            };

            // Save the document as PPTX using the configured options
            doc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
