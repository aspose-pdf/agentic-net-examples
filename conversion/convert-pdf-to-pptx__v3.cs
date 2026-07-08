using System;
using System.IO;
using Aspose.Pdf; // Document, SaveFormat

class Program
{
    static void Main()
    {
        // Input PDF and output PPTX paths
        const string pdfPath = "input.pdf";
        const string outputPptx = "output.pptx";

        // Validate input file
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        // Aspose.Pdf can directly save a PDF document as a PPTX file.
        // The original requirement to apply a custom slide master would need
        // Aspose.Slides, which is not referenced in this project. Therefore the
        // conversion is performed without master‑template manipulation.
        using (Document pdfDoc = new Document(pdfPath))
        {
            pdfDoc.Save(outputPptx, SaveFormat.Pptx);
        }

        Console.WriteLine($"Conversion complete. Output saved to '{outputPptx}'.");
    }
}
