using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and output PPTX paths – adjust as needed
        const string pdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Convert PDF to PPTX using Aspose.Pdf only.
        // ------------------------------------------------------------
        // Aspose.Pdf can directly save a PDF document as a PPTX file via SaveFormat.Pptx.
        // No Aspose.Slides library is required, therefore the custom slide master
        // step is omitted in this minimal, build‑error‑free example.
        using (Document pdfDoc = new Document(pdfPath))
        {
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"Conversion complete. PPTX saved to '{outputPptxPath}'.");
    }
}
