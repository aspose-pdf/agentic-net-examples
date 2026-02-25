using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
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
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                // Example setting: render each slide as an image
                SlidesAsImages = true
            };

            // Attach a custom progress handler to receive conversion events
            pptxOptions.CustomProgressHandler = new Aspose.Pdf.UnifiedSaveOptions.ConversionProgressEventHandler(ShowProgressOnConsole);

            // Perform the conversion and save as PPTX
            pdfDoc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }

    // Custom progress handler method
    private static void ShowProgressOnConsole(Aspose.Pdf.UnifiedSaveOptions.ProgressEventHandlerInfo eventInfo)
    {
        switch (eventInfo.EventType)
        {
            case Aspose.Pdf.ProgressEventType.TotalProgress:
                Console.WriteLine($"{DateTime.Now:T} - Total progress: {eventInfo.Value}%");
                break;
            case Aspose.Pdf.ProgressEventType.SourcePageAnalysed:
                Console.WriteLine($"{DateTime.Now:T} - Source page {eventInfo.Value} of {eventInfo.MaxValue} analysed.");
                break;
            case Aspose.Pdf.ProgressEventType.ResultPageCreated:
                Console.WriteLine($"{DateTime.Now:T} - Result page {eventInfo.Value} of {eventInfo.MaxValue} created.");
                break;
            case Aspose.Pdf.ProgressEventType.ResultPageSaved:
                Console.WriteLine($"{DateTime.Now:T} - Result page {eventInfo.Value} of {eventInfo.MaxValue} saved.");
                break;
            default:
                // Other events can be handled here if needed
                break;
        }
    }
}