using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Progress handler that writes conversion progress information to the console
    static void ShowProgress(UnifiedSaveOptions.ProgressEventHandlerInfo info)
    {
        // Simple generic logging – the EventType enum values are version‑specific, so we just output the enum name.
        Console.WriteLine($"{DateTime.Now:T} - Event: {info.EventType}, Value: {info.Value}, MaxValue: {info.MaxValue}");
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";   // Path to the source PDF
        const string outputPptx = "output.pptx"; // Desired PPTX output path

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document and ensure proper disposal
            using (Document pdfDocument = new Document(inputPdf))
            {
                // Initialize PPTX save options and attach the custom progress handler
                PptxSaveOptions saveOptions = new PptxSaveOptions
                {
                    CustomProgressHandler = new PptxSaveOptions.ConversionProgressEventHandler(ShowProgress)
                };

                // Save the document as PPTX using the specified options
                pdfDocument.Save(outputPptx, saveOptions);
            }

            Console.WriteLine($"Conversion completed. PPTX saved to '{outputPptx}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
