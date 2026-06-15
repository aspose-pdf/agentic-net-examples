using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    // Progress handler method – matches the delegate signature expected by PptxSaveOptions
    private static void ShowProgress(PptxSaveOptions.ProgressEventHandlerInfo info)
    {
        // Display generic progress information on the console.
        // The specific enum values are not referenced directly to avoid version‑specific issues.
        Console.WriteLine($"Progress event: {info.EventType}, Value: {info.Value}, MaxValue: {info.MaxValue}");
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

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize PPTX save options and attach the custom progress handler
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                pptxOptions.CustomProgressHandler =
                    new PptxSaveOptions.ConversionProgressEventHandler(ShowProgress);

                // Save the document as PPTX using the configured options
                pdfDocument.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
