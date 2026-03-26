using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Convert PDF to PPTX using Aspose.Pdf only. No Aspose.Slides dependency is required.
        using (Document pdfDoc = new Document(inputPdf))
        {
            pdfDoc.Save(outputPptx, SaveFormat.Pptx);
        }

        Console.WriteLine($"Conversion complete. PPTX saved to '{outputPptx}'.");
    }
}
