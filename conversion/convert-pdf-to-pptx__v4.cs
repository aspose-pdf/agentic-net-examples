using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxAndExtractImages
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Validate input PDF
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Convert PDF to PPTX using Aspose.Pdf's SaveFormat
        // -------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Directly save the PDF as a PPTX file – no separate SaveOptions class is required
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine("Conversion completed: PDF -> PPTX");
    }
}
