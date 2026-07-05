using System;
using System.IO;
using Aspose.Pdf; // Core PDF handling – provides Document, SaveFormat, PptxSaveOptions

class Program
{
    static void Main()
    {
        // Paths for input PDF and final PPTX (no intermediate file needed)
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Convert PDF to PPTX using Aspose.Pdf only
        // ------------------------------------------------------------
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Simple conversion – format is inferred from the file extension
            pdfDocument.Save(outputPptxPath, SaveFormat.Pptx);

            // If you need to tweak conversion options, use PptxSaveOptions instead:
            // var pptxOptions = new PptxSaveOptions();
            // pdfDocument.Save(outputPptxPath, pptxOptions);
        }

        // NOTE: Adding speaker notes to the generated PPTX requires the Aspose.Slides library.
        // Because the project is restricted to Aspose.Pdf only, speaker‑note manipulation is omitted.

        Console.WriteLine($"PDF successfully converted to PPTX: '{outputPptxPath}'.");
    }
}
