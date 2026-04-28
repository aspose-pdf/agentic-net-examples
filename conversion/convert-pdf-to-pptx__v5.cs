using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Aspose.Pdf can directly save a PDF as PPTX; no Aspose.Slides assembly is required.
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"PDF successfully converted to PPTX and saved as '{outputPptxPath}'.");
    }
}
