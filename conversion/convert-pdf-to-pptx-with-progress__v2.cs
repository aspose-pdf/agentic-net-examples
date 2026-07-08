using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Progress handler method matching the expected delegate signature.
    // The delegate type is Aspose.Pdf.PptxSaveOptions.ConversionProgressEventHandler
    // and the event info type is Aspose.Pdf.PptxSaveOptions.ProgressEventHandlerInfo.
    static void ShowProgressOnConsole(Aspose.Pdf.PptxSaveOptions.ProgressEventHandlerInfo eventInfo)
    {
        // Log generic progress information. Specific enum values are not referenced
        // because the ProgressEventType enum is not exposed in the current SDK version.
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} - {eventInfo.EventType}: {eventInfo.Value}% (max {eventInfo.MaxValue}).");
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

        // Load the PDF document inside a using block to ensure proper disposal.
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Initialize save options for PPTX conversion.
            Aspose.Pdf.PptxSaveOptions pptxOptions = new Aspose.Pdf.PptxSaveOptions();

            // Assign the custom progress handler.
            pptxOptions.CustomProgressHandler = ShowProgressOnConsole;

            // Save the document as PPTX using the configured options.
            pdfDoc.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: '{outputPptxPath}'.");
    }
}
