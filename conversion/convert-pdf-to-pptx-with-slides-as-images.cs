using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Progress handler matching the delegate expected by PptxSaveOptions.CustomProgressHandler
    private static void ProgressHandler(UnifiedSaveOptions.ProgressEventHandlerInfo info)
    {
        // Use the information provided by the ProgressEventHandlerInfo
        switch (info.EventType)
        {
            case ProgressEventType.TotalProgress:
                // Value represents the overall percentage completed
                Console.WriteLine($"Conversion progress: {info.Value}%");
                break;
            case ProgressEventType.ResultPageSaved:
                // Value is the current page index, MaxValue is the total page count
                Console.WriteLine($"Saved page {info.Value} of {info.MaxValue}");
                break;
            default:
                // For other event types you can output generic info if desired
                Console.WriteLine($"Event {info.EventType}: {info.Value}/{info.MaxValue}");
                break;
        }
    }

    static void Main()
    {
        const string inputPath = "brochure.pdf";
        const string outputPath = "brochure_full.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure PPTX save options: render each slide as an image and attach progress handler
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true,
                CustomProgressHandler = ProgressHandler
            };

            // Save as PPTX using explicit save options (required for non‑PDF formats)
            doc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed. PPTX saved to '{outputPath}'.");
    }
}
