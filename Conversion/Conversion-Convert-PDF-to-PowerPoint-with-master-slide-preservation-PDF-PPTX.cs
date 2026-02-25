using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API and all SaveOptions subclasses
using Aspose.Pdf.Devices;      // For PptxSaveOptions (also resides in Aspose.Pdf namespace)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize save options for PPTX conversion
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Preserve master slide layout (default behavior). Additional options can be set if needed:
            // pptxOptions.SlidesAsImages = false; // keep editable shapes when possible

            // Save the PDF as PPTX using the explicit save options (required for non‑PDF output)
            pdfDoc.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}