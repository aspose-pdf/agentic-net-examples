using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Convert PDF to PPTX using Aspose.Pdf's built‑in conversion.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // PptxSaveOptions resides directly in the Aspose.Pdf namespace.
                var pptxOptions = new PptxSaveOptions();
                pdfDoc.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}