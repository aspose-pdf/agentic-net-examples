using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        // Aspose.Pdf can directly save a PDF document as a PPTX file.
        using (Document pdfDoc = new Document(pdfPath))
        {
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"PDF converted to PPTX: {pptxPath}");
    }
}
