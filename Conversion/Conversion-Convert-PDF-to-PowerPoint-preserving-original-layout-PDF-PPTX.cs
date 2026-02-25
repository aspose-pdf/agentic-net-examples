using System;
using System.IO;
using Aspose.Pdf;                 // Document, PptxSaveOptions
using Aspose.Pdf.Facades;        // (facade namespace, not used directly here but kept for completeness)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPptx = "output.pptx";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Wrap Document in a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize PPTX save options (required for non‑PDF output)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as PPTX while preserving layout
                pdfDoc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputPptx}'");
        }
        // GDI+ is required for some conversion paths; handle Windows‑only limitation gracefully
        catch (TypeInitializationException)
        {
            Console.WriteLine("PDF‑to‑PPTX conversion requires Windows GDI+. Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}