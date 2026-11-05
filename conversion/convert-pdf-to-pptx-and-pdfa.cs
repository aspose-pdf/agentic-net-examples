using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";
        const string outputPdfaPath = "archival_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // 1. Convert PDF to PPTX using Aspose.Pdf (no Aspose.Slides required)
        using (var pdfDoc = new Document(inputPdfPath))
        {
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        // 2. Convert the original PDF to PDF/A‑1b (archival format) using Aspose.Pdf
        using (var pdfDoc = new Document(inputPdfPath))
        {
            // Convert to PDF/A‑1b; the first argument is the output file path
            pdfDoc.Convert(outputPdfaPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
        }

        Console.WriteLine("Conversion completed:");
        Console.WriteLine($"PPTX file: {outputPptxPath}");
        Console.WriteLine($"PDF/A file: {outputPdfaPath}");
    }
}