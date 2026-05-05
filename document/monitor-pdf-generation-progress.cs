using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create save options that support a custom progress handler
            DocSaveOptions saveOptions = new DocSaveOptions();

            // Assign the progress callback – this will be invoked during the save operation
            saveOptions.CustomProgressHandler = new DocSaveOptions.ConversionProgressEventHandler(ShowProgress);

            // Save the document as PDF while the progress handler reports updates
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine("PDF generation completed.");
    }

    // Callback method matching the required delegate signature
    private static void ShowProgress(DocSaveOptions.ProgressEventHandlerInfo eventInfo)
    {
        // Use the ProgressEventType enum to differentiate the stages
        switch (eventInfo.EventType)
        {
            case Aspose.Pdf.ProgressEventType.TotalProgress:
                // eventInfo.Value is a percentage (0‑100)
                Console.WriteLine($"Total progress: {eventInfo.Value}%");
                break;

            case Aspose.Pdf.ProgressEventType.SourcePageAnalysed:
                Console.WriteLine($"Source page {eventInfo.Value} of {eventInfo.MaxValue} analysed.");
                break;

            case Aspose.Pdf.ProgressEventType.ResultPageCreated:
                Console.WriteLine($"Result page {eventInfo.Value} of {eventInfo.MaxValue} created.");
                break;

            case Aspose.Pdf.ProgressEventType.ResultPageSaved:
                Console.WriteLine($"Result page {eventInfo.Value} of {eventInfo.MaxValue} saved.");
                break;

            default:
                // No action needed for other event types
                break;
        }
    }
}