using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "agenda.pdf";
        const string outputPath = "agenda_full.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure PPTX save options: render each slide as an image and attach a custom progress handler
            var pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true,
                // Attach a custom progress handler – the delegate receives a ProgressEventHandlerInfo instance
                CustomProgressHandler = info =>
                {
                    // Simple console logging of progress events
                    Console.WriteLine($"Progress: {info.EventType} – {info.Value}% (max {info.MaxValue})");
                }
            };

            // Save the PDF as PPTX using the configured options
            doc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
