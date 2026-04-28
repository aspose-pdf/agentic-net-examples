using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Progress handler that will be called during conversion.
    // It receives information about the conversion progress.
    static void ShowProgress(UnifiedSaveOptions.ProgressEventHandlerInfo info)
    {
        // Directly display the conversion progress percentage.
        // The specific event type enum is not required for basic monitoring.
        Console.WriteLine($"{DateTime.Now:T} - Conversion progress: {info.Value}%");
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Initialize PPTX save options.
            PptxSaveOptions saveOptions = new PptxSaveOptions();

            // Assign the custom progress handler.
            saveOptions.CustomProgressHandler = new PptxSaveOptions.ConversionProgressEventHandler(ShowProgress);

            // Save the document as PPTX using the configured options.
            pdfDocument.Save(outputPptx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
    }
}
