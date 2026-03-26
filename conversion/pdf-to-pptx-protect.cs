using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";
        // Password protection of PPTX requires Aspose.Slides. Since the Slides library is not referenced,
        // the conversion is performed with Aspose.Pdf only. If PPTX protection is required, add the
        // Aspose.Slides NuGet package and use its ProtectionManager as shown in the original example.

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Convert PDF to PPTX using Aspose.Pdf's native support.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}
