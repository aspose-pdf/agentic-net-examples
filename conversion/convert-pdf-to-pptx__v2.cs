using System;
using System.IO;
using Aspose.Pdf; // Core PDF API

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

        // Convert PDF to PPTX using Aspose.Pdf (no Aspose.Slides required)
        using (Document pdfDoc = new Document(pdfPath))
        {
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"PPTX file saved at '{pptxPath}'.");
    }
}
