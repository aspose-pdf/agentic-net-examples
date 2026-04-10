using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialize PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Assign a custom progress handler to receive conversion events
            // The delegate signature matches ShowProgress, so we can assign it directly
            pptxOptions.CustomProgressHandler = ShowProgress;

            // Save the document as PPTX using the configured options
            pdfDoc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
    }

    // Custom progress handler method
    static void ShowProgress(PptxSaveOptions.ProgressEventHandlerInfo eventInfo)
    {
        // Log generic progress information. The specific enum values are not required for compilation.
        // eventInfo.EventType provides a meaningful description of the event.
        Console.WriteLine($"{DateTime.Now:T} - {eventInfo.EventType}: {eventInfo.Value}% (Max: {eventInfo.MaxValue})");
    }
}
