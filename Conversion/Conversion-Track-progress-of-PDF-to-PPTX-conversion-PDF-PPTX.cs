using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Progress handler – matches the delegate Aspose.Pdf.UnifiedSaveOptions.ConversionProgressEventHandler
    private static void ShowProgressOnConsole(Aspose.Pdf.UnifiedSaveOptions.ProgressEventHandlerInfo eventInfo)
    {
        // The event type is Aspose.Pdf.ProgressEventType
        switch (eventInfo.EventType)
        {
            case Aspose.Pdf.ProgressEventType.TotalProgress:
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Conversion progress: {eventInfo.Value}%.");
                break;

            case Aspose.Pdf.ProgressEventType.SourcePageAnalysed:
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Source page {eventInfo.Value} of {eventInfo.MaxValue} analysed.");
                break;

            case Aspose.Pdf.ProgressEventType.ResultPageCreated:
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Result page {eventInfo.Value} of {eventInfo.MaxValue} layout created.");
                break;

            case Aspose.Pdf.ProgressEventType.ResultPageSaved:
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Result page {eventInfo.Value} of {eventInfo.MaxValue} exported.");
                break;

            default:
                // No action for other events
                break;
        }
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdf))
            {
                // Configure PPTX save options
                PptxSaveOptions saveOptions = new PptxSaveOptions
                {
                    // Attach the progress handler
                    CustomProgressHandler = new Aspose.Pdf.UnifiedSaveOptions.ConversionProgressEventHandler(ShowProgressOnConsole)
                };

                // Perform the conversion
                pdfDocument.Save(outputPptx, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: '{outputPptx}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}