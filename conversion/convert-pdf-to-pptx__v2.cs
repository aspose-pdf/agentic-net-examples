using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptx
{
    static void Main()
    {
        // Paths for input PDF and output PPTX
        const string pdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Convert PDF to PPTX using Aspose.Pdf only (no Aspose.Slides required)
        using (var pdfDocument = new Document(pdfPath))
        {
            pdfDocument.Save(outputPptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"Conversion completed. Output saved to '{outputPptxPath}'.");
    }
}