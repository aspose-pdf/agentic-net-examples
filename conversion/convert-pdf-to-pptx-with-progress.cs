using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Simple progress handler that works with any version of Aspose.Pdf.
    // It prints the progress value reported by the conversion process.
    static void ShowProgress(PptxSaveOptions.ProgressEventHandlerInfo eventInfo)
    {
        // The ProgressEventHandlerInfo always contains a Value property (percentage of work done).
        // Some older or newer library versions may not expose an EventType enum, so we avoid using it.
        Console.WriteLine($"{DateTime.Now:T} - Conversion progress: {eventInfo.Value}%");
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialise PPTX save options and attach the custom progress handler.
            PptxSaveOptions saveOptions = new PptxSaveOptions();
            saveOptions.CustomProgressHandler = new PptxSaveOptions.ConversionProgressEventHandler(ShowProgress);

            // Save the document as PPTX using the configured options.
            pdfDocument.Save(outputPptxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed. PPTX saved to '{outputPptxPath}'.");
    }
}
