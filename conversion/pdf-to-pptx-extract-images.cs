using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and output PPTX paths
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        // Aspose.Pdf can directly save a PDF document as PPTX using SaveFormat.Pptx.
        using (Document pdfDoc = new Document(pdfPath))
        {
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }
        Console.WriteLine($"PDF converted to PPTX: {pptxPath}");

        // NOTE: Extracting images from the generated PPTX requires the Aspose.Slides library.
        // If Aspose.Slides is added to the project, the image‑extraction code can be re‑introduced.
    }
}
