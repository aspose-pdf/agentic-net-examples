using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace provides Document, SaveFormat, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        // Aspose.Pdf can directly save a PDF document as a PPTX file using SaveFormat.Pptx.
        // No separate SaveOptions class is required for this conversion.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        // NOTE: Adding speaker notes to the generated PPTX requires the Aspose.Slides library.
        // Since the current project does not reference Aspose.Slides, that step is omitted.
        // If speaker‑note functionality is needed, add a reference to the Aspose.Slides NuGet package
        // and use its Presentation API to open the PPTX, create notes slides, and save again.

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}
