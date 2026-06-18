using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Progress handler that will be called during conversion.
    // The handler receives information about the current event.
    static void ShowProgress(PptxSaveOptions.ProgressEventHandlerInfo eventInfo)
    {
        // Simple console logging – you can customize the output as needed.
        Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Event: {eventInfo.EventType}, " +
                          $"Value: {eventInfo.Value}/{eventInfo.MaxValue}");
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";   // Path to source PDF
        const string outputPptx = "output.pptx"; // Desired PPTX output path

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize PPTX save options and attach the custom progress handler
                PptxSaveOptions saveOptions = new PptxSaveOptions();
                saveOptions.CustomProgressHandler =
                    new PptxSaveOptions.ConversionProgressEventHandler(ShowProgress);

                // Save the document as PPTX using the options (non‑PDF format requires SaveOptions)
                pdfDoc.Save(outputPptx, saveOptions);
            }

            Console.WriteLine($"Conversion completed. PPTX saved to '{outputPptx}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}