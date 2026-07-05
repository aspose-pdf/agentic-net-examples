using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Prepare save options for HTML conversion
            Aspose.Pdf.HtmlSaveOptions saveOptions = new Aspose.Pdf.HtmlSaveOptions();

            // Attach a custom progress handler (uses UnifiedSaveOptions delegate)
            saveOptions.CustomProgressHandler = new Aspose.Pdf.UnifiedSaveOptions.ConversionProgressEventHandler(ShowProgress);

            // Save the document (lifecycle rule: use Document.Save with SaveOptions)
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine("Conversion completed.");
    }

    // Progress handler invoked by the converter
    private static void ShowProgress(Aspose.Pdf.UnifiedSaveOptions.ProgressEventHandlerInfo info)
    {
        // Simple console‑based progress feedback (replace with UI progress bar as needed)
        switch (info.EventType)
        {
            case Aspose.Pdf.ProgressEventType.TotalProgress:
                Console.WriteLine($"Total progress: {info.Value}%");
                break;
            case Aspose.Pdf.ProgressEventType.SourcePageAnalysed:
                Console.WriteLine($"Source page {info.Value} of {info.MaxValue} analysed.");
                break;
            case Aspose.Pdf.ProgressEventType.ResultPageCreated:
                Console.WriteLine($"Result page {info.Value} of {info.MaxValue} created.");
                break;
            case Aspose.Pdf.ProgressEventType.ResultPageSaved:
                Console.WriteLine($"Result page {info.Value} of {info.MaxValue} saved.");
                break;
            default:
                // No action for other event types
                break;
        }
    }
}