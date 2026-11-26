using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "contract.pdf";
        const string outputPath = "contract_progress.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure PPTX save options and assign a custom progress handler.
            var pptxOptions = new PptxSaveOptions();

            // The CustomProgressHandler delegate receives a ProgressEventHandlerInfo object.
            pptxOptions.CustomProgressHandler = info =>
            {
                // Show progress for the most relevant events.
                if (info.EventType == ProgressEventType.TotalProgress ||
                    info.EventType == ProgressEventType.ResultPageSaved)
                {
                    Console.WriteLine($"Conversion progress: {info.Value}% (event: {info.EventType})");
                }
            };

            // Save the document as PPTX using the configured options.
            pdfDoc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
    }
}
