using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Source PDF
        const string pptxPath = "output.pptx";        // Resulting PPTX file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Aspose.Pdf can directly save a PDF as PPTX using SaveFormat.Pptx.
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"Conversion complete. PPTX saved to '{pptxPath}'.");
    }
}
