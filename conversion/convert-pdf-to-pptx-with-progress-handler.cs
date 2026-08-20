using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Progress handler that will be called during conversion.
    // It receives information about the current progress event.
    static void ShowProgress(Aspose.Pdf.UnifiedSaveOptions.ProgressEventHandlerInfo info)
    {
        // Simple console output showing event type and its value.
        Console.WriteLine($"{info.EventType}: {info.Value}/{info.MaxValue}");
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize PPTX save options and assign the custom progress handler.
            PptxSaveOptions saveOptions = new PptxSaveOptions();
            saveOptions.CustomProgressHandler = new PptxSaveOptions.ConversionProgressEventHandler(ShowProgress);

            // Save the document as PPTX using the specified options.
            pdfDocument.Save(outputPptxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed. PPTX saved to '{outputPptxPath}'.");
    }
}