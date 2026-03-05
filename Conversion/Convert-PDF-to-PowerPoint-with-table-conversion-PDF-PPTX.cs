using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Devices;      // Contains PptxSaveOptions (also in Aspose.Pdf)

class PdfToPptxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output PPTX file path
        const string pptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create + load)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Initialize save options for PPTX conversion.
                // PptxSaveOptions derives from UnifiedSaveOptions and is the
                // required SaveOptions subclass for non‑PDF output.
                PptxSaveOptions saveOptions = new PptxSaveOptions();

                // Optional: enable table detection/optimization if desired.
                // saveOptions.OptimizeTextBoxes = true; // uncomment to improve table handling

                // Save the document as PPTX (lifecycle: save)
                pdfDocument.Save(pptxPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}