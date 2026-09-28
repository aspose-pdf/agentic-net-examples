using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string intermediatePptxPath = "intermediate.pptx";
        const string pdfAPath       = "archival_output.pdf";
        const string conversionLog  = "conversion_log.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Load the source PDF and save it as PPTX
        // -------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Save as PowerPoint (PPTX) using PptxSaveOptions
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            pdfDoc.Save(intermediatePptxPath, pptxOptions);
        }

        // -------------------------------------------------
        // Step 2: Load the generated PPTX and convert to PDF/A
        // -------------------------------------------------
        if (!File.Exists(intermediatePptxPath))
        {
            Console.Error.WriteLine($"Failed to create PPTX: {intermediatePptxPath}");
            return;
        }

        using (Document pptxDoc = new Document(intermediatePptxPath))
        {
            // Convert to PDF/A (PDF/A-1B) and log any conversion issues
            pptxDoc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the resulting PDF/A file
            pptxDoc.Save(pdfAPath);
        }

        Console.WriteLine($"Conversion complete. PDF/A saved to '{pdfAPath}'.");
    }
}