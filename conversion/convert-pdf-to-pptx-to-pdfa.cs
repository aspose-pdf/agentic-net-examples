using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string intermediatePptx = "intermediate.pptx";
        const string outputPdfA = "archival.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Step 1: Convert PDF to PPTX
            using (Document pdfDocument = new Document(inputPdf))
            {
                var pptxOptions = new PptxSaveOptions();
                pdfDocument.Save(intermediatePptx, pptxOptions);
            }

            // Step 2: Convert PPTX to PDF/A‑1B
            using (Document pptxDocument = new Document(intermediatePptx))
            {
                pptxDocument.Convert("conversion.log", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                pptxDocument.Save(outputPdfA);
            }

            Console.WriteLine($"Conversion complete. PPTX: {intermediatePptx}, PDF/A: {outputPdfA}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}