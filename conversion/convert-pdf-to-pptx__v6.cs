using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "presentation.pdf";
        const string outputPath = "presentation.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Explicitly specify PPTX save options; otherwise the file would be saved as PDF
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            pdfDoc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
    }
}