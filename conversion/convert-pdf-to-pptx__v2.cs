using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";
        // Note: Password protection for PPTX files requires Aspose.Slides, which is not referenced in this project.
        // The conversion is performed using Aspose.Pdf only.

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Convert PDF to PPTX directly using Aspose.Pdf's built‑in support.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: '{outputPptxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
