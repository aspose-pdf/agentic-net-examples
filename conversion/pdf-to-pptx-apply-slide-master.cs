using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputPdf = "input.pdf";
        string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Convert PDF to PPTX using Aspose.Pdf (no Aspose.Slides required)
        using (Document pdfDoc = new Document(inputPdf))
        {
            pdfDoc.Save(outputPptx, SaveFormat.Pptx);
        }

        Console.WriteLine($"Converted PDF to PPTX: '{outputPptx}'");
    }
}
