using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "converted.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // -------------------------------------------------
        // Step: Convert PDF to PPTX using Aspose.Pdf only
        // -------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Aspose.Pdf can directly save a PDF as PPTX – no need for Aspose.Slides.
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        // NOTE: Adding speaker notes to the generated PPTX would require the
        // Aspose.Slides library, which is not referenced in this project. The
        // conversion itself is performed entirely with Aspose.Pdf.

        Console.WriteLine($"PDF successfully converted to PPTX.\nOutput file: {outputPptxPath}");
    }
}
