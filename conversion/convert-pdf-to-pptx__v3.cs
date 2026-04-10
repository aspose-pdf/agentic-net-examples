using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and output PPTX file paths
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";

        // ------------------------------------------------------------
        // Ensure the source PDF exists – create a simple one if missing
        // ------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            using (var doc = new Document())
            {
                // Add a blank page (or you can add any content you need)
                doc.Pages.Add();
                doc.Save(pdfPath);
                Console.WriteLine($"Sample PDF created at '{pdfPath}'.");
            }
        }

        // ------------------- Convert PDF to PPTX -------------------
        using (var pdfDoc = new Document(pdfPath))
        {
            // Save as PPTX using Aspose.Pdf's built‑in support
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }
        Console.WriteLine("PDF converted to PPTX successfully.");

        // NOTE: Adding speaker notes requires the Aspose.Slides library, which is a separate product.
        // Since the current project does not reference Aspose.Slides, the note‑adding step has been omitted.
    }
}
