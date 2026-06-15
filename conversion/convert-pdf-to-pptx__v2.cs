using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        // Aspose.Pdf can directly save a PDF as PPTX using SaveFormat.Pptx
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        // NOTE: Password‑protecting a PPTX file requires Aspose.Slides.
        // If password protection is required, add a reference to the Aspose.Slides
        // NuGet package and use Presentation.ProtectionManager.Encrypt(...).
        // The current project builds without Aspose.Slides, so only the conversion
        // step is performed here.

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}
