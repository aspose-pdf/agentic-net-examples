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

        // Validate input PDF
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // -------------------------------------------------
        // Convert PDF to PPTX using Aspose.Pdf only.
        // -------------------------------------------------
        // Aspose.Slides is a separate product; the current project does not reference it.
        // Therefore the slide‑image extraction part is omitted. The conversion itself is fully
        // supported by Aspose.Pdf via the SaveFormat.Pptx option.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine("PDF conversion to PPTX completed successfully.");
    }
}
