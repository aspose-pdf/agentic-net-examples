using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Progress handler that writes conversion progress to the console
    private static void ShowProgressOnConsole(Aspose.Pdf.UnifiedSaveOptions.ProgressEventHandlerInfo eventInfo)
    {
        switch (eventInfo.EventType)
        {
            case Aspose.Pdf.ProgressEventType.TotalProgress:
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Conversion progress: {eventInfo.Value}%.");
                break;
            case Aspose.Pdf.ProgressEventType.SourcePageAnalysed:
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Source page {eventInfo.Value} of {eventInfo.MaxValue} analysed.");
                break;
            case Aspose.Pdf.ProgressEventType.ResultPageCreated:
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Result page {eventInfo.Value} of {eventInfo.MaxValue} created.");
                break;
            case Aspose.Pdf.ProgressEventType.ResultPageSaved:
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Result page {eventInfo.Value} of {eventInfo.MaxValue} saved.");
                break;
            default:
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

        // Load the PDF and convert it to PPTX while tracking progress
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure PPTX save options
            PptxSaveOptions saveOptions = new PptxSaveOptions
            {
                // Example: export each slide as an image (optional)
                SlidesAsImages = false,
                // Attach the custom progress handler
                CustomProgressHandler = new Aspose.Pdf.UnifiedSaveOptions.ConversionProgressEventHandler(ShowProgressOnConsole)
            };

            // Perform the conversion
            pdfDocument.Save(outputPptx, saveOptions);
        }

        Console.WriteLine($"Conversion completed. PPTX saved to '{outputPptx}'.");
    }
}