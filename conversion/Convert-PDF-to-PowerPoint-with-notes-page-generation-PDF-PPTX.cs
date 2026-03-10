using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document and PptxSaveOptions)

class PdfToPptxConverter
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // Source PDF file
        const string outputPptxPath = "output.pptx"; // Destination PowerPoint file

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create/load)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize save options for PPTX conversion (must pass explicit options)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as a PowerPoint file with notes pages (if supported by the converter)
                pdfDoc.Save(outputPptxPath, pptxOptions); // lifecycle: save
            }

            Console.WriteLine($"Conversion successful: '{outputPptxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}