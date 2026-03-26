using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";
        const string outputPdfA = "output_pdfa.pdf";
        const string conversionLog = "conversion.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Step 1: Load PDF and save as PPTX (non‑PDF format requires explicit SaveOptions)
        using (Document pdfDoc = new Document(inputPdf))
        {
            PptxSaveOptions pptxOpts = new PptxSaveOptions();
            pdfDoc.Save(outputPptx, pptxOpts);
        }

        if (!File.Exists(outputPptx))
        {
            Console.Error.WriteLine($"Failed to create PPTX: {outputPptx}");
            return;
        }

        // Step 2: Load the generated PPTX and convert to PDF/A for archival
        using (Document pptxDoc = new Document(outputPptx))
        {
            // Convert to PDF/A‑1B, logging any conversion issues
            pptxDoc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
            // Save the resulting PDF/A document
            pptxDoc.Save(outputPdfA);
        }

        Console.WriteLine($"Conversion complete. PPTX: {outputPptx}, PDF/A: {outputPdfA}");
    }
}