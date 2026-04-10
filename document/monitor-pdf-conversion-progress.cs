using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Progress handler that receives conversion progress information.
    // It updates the console (simulating a UI progress bar) based on the event type.
    private static void ShowProgress(Aspose.Pdf.UnifiedSaveOptions.ProgressEventHandlerInfo eventInfo)
    {
        // The ProgressEventType enum indicates what stage the conversion is in.
        switch (eventInfo.EventType)
        {
            case Aspose.Pdf.ProgressEventType.TotalProgress:
                // Overall percentage of the conversion.
                Console.WriteLine($"Total progress: {eventInfo.Value}%");
                break;

            case Aspose.Pdf.ProgressEventType.SourcePageAnalysed:
                // A source page has been analysed.
                Console.WriteLine($"Source page {eventInfo.Value} of {eventInfo.MaxValue} analysed.");
                break;

            case Aspose.Pdf.ProgressEventType.ResultPageCreated:
                // A result page layout has been created.
                Console.WriteLine($"Result page {eventInfo.Value} of {eventInfo.MaxValue} layout created.");
                break;

            case Aspose.Pdf.ProgressEventType.ResultPageSaved:
                // A result page has been saved/exported.
                Console.WriteLine($"Result page {eventInfo.Value} of {eventInfo.MaxValue} saved.");
                break;

            default:
                // Other events are ignored.
                break;
        }
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Initialize HtmlSaveOptions and attach the custom progress handler.
            Aspose.Pdf.HtmlSaveOptions saveOptions = new Aspose.Pdf.HtmlSaveOptions();
            // The field is public; assign the delegate directly.
            saveOptions.CustomProgressHandler = new Aspose.Pdf.UnifiedSaveOptions.ConversionProgressEventHandler(ShowProgress);

            // Save the document as HTML while progress events are reported.
            doc.Save(outputHtmlPath, saveOptions);
        }

        Console.WriteLine("Conversion completed.");
    }
}