using System;
using System.IO;
using Aspose.Pdf;               // Document, PptxSaveOptions
using Aspose.Pdf.Facades;      // Included as requested (no Facade class needed for this conversion)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (creation rule)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize PPTX save options (save-to-non-pdf-always-use-save-options rule)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as PPTX using the explicit options (required for non‑PDF formats)
                pdfDoc.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputPptxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}