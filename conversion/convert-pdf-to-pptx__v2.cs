using System;
using System.IO;
using Aspose.Pdf; // Provides Document, SaveFormat, etc.

class Program
{
    static void Main()
    {
        // Paths
        const string pdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Validate input PDF
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Convert PDF to PPTX using Aspose.Pdf
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Directly save as PPTX – no separate SaveOptions class needed
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"Conversion complete. Output saved to '{outputPptxPath}'.");
        // NOTE: Applying a custom slide master template requires the Aspose.Slides library.
        // If you have Aspose.Slides referenced, you can load the generated PPTX and replace its master slides as shown in the original example.
    }
}
