using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf;               // PptxSaveOptions resides in this namespace

class PdfToPptxConverter
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize save options for PPTX (master slide is preserved automatically)
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save as PPTX using explicit SaveOptions (required for non‑PDF formats)
            pdfDoc.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}