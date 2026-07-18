using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Progress handler method – logs conversion progress to the console
    private static void ShowProgress(UnifiedSaveOptions.ProgressEventHandlerInfo info)
    {
        // Example: log total progress percentage
        Console.WriteLine($"{DateTime.Now:T} - Progress: {info.EventType} = {info.Value}/{info.MaxValue}");
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize PPTX save options
            PptxSaveOptions saveOptions = new PptxSaveOptions();

            // Assign the custom progress handler
            saveOptions.CustomProgressHandler = new PptxSaveOptions.ConversionProgressEventHandler(ShowProgress);

            // Save the document as PPTX using the configured options
            pdfDoc.Save(outputPptx, saveOptions);
        }

        Console.WriteLine($"Conversion completed: {outputPptx}");
    }
}