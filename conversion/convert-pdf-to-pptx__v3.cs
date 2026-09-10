using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source PDF and destination PPTX
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        // Aspose.Pdf can directly save a PDF document as PPTX using SaveFormat.Pptx
        using (Document pdfDoc = new Document(pdfPath))
        {
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // Verify that the PPTX was created
        if (File.Exists(pptxPath))
        {
            Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");
        }
        else
        {
            Console.Error.WriteLine($"Failed to create PPTX file: {pptxPath}");
        }
    }
}
