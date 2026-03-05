using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the destination PPTX file
        const string inputPdf  = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize save options for PPTX conversion
            PptxSaveOptions saveOptions = new PptxSaveOptions();

            // Optional: export each slide as a single image
            // saveOptions.SlidesAsImages = true;

            // Attach a custom progress handler to receive conversion events
            saveOptions.CustomProgressHandler = new PptxSaveOptions.ConversionProgressEventHandler(ShowProgress);

            // Perform the conversion and save the PPTX file
            pdfDoc.Save(outputPptx, saveOptions);
        }

        Console.WriteLine($"PDF → PPTX conversion completed: {outputPptx}");
    }

    // Progress handler invoked by Aspose.Pdf during conversion
    private static void ShowProgress(UnifiedSaveOptions.ProgressEventHandlerInfo eventInfo)
    {
        switch (eventInfo.EventType)
        {
            case ProgressEventType.TotalProgress:
                Console.WriteLine($"{DateTime.Now:T} - Total progress: {eventInfo.Value}%");
                break;
            case ProgressEventType.SourcePageAnalysed:
                Console.WriteLine($"{DateTime.Now:T} - Source page {eventInfo.Value} of {eventInfo.MaxValue} analysed.");
                break;
            case ProgressEventType.ResultPageCreated:
                Console.WriteLine($"{DateTime.Now:T} - Result page {eventInfo.Value} of {eventInfo.MaxValue} created.");
                break;
            case ProgressEventType.ResultPageSaved:
                Console.WriteLine($"{DateTime.Now:T} - Result page {eventInfo.Value} of {eventInfo.MaxValue} saved.");
                break;
            default:
                // Other events can be handled here if needed
                break;
        }
    }
}