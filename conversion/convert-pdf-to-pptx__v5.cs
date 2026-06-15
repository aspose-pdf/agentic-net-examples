using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace provides Document and SaveFormat

class Program
{
    static void Main()
    {
        // Input PDF and output PPTX paths
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";

        // -------------------------------------------------
        // 1. Validate input PDF existence
        // -------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // -------------------------------------------------
        // 2. Convert PDF to PPTX using Aspose.Pdf only
        // -------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Directly save as PPTX – no separate PptxSaveOptions class is required
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"Conversion complete. PPTX saved to '{pptxPath}'.");
    }
}
