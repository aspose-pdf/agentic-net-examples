using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "proposal.pdf";
        const string outputPath = "proposal_full.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure PPTX save options
            var pptxOptions = new PptxSaveOptions
            {
                // Render each slide as an image to preserve appearance
                SlidesAsImages = true,
                // Attach a custom progress handler (UnifiedSaveOptions.CustomProgressHandler)
                CustomProgressHandler = info =>
                {
                    // info.EventType describes the kind of progress event
                    switch (info.EventType)
                    {
                        case ProgressEventType.TotalProgress:
                            Console.WriteLine($"Conversion progress: {info.Value:0.00}%");
                            break;
                        case ProgressEventType.ResultPageSaved:
                            Console.WriteLine($"Saved page {info.Value} of {info.MaxValue}");
                            break;
                    }
                }
            };

            // Save the document as PPTX using the configured options
            doc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
    }
}
