using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "minutes.pdf";
        const string outputPath = "minutes_full.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure PPTX save options with progress reporting
            var pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true,
                // The CustomProgressHandler expects a delegate that receives a ProgressEventHandlerInfo
                CustomProgressHandler = info =>
                {
                    // Report only total progress; other event types can be handled similarly
                    if (info.EventType == ProgressEventType.TotalProgress)
                    {
                        Console.WriteLine($"Conversion progress: {info.Value}%");
                    }
                }
            };

            // Save the PDF as a PPTX using the configured options
            pdfDoc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed. Output saved to '{outputPath}'.");
    }
}
