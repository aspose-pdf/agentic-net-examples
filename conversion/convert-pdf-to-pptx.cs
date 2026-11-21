using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Default options for PPTX conversion.
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Save as PPTX. Explicit SaveOptions are required for non‑PDF formats.
            pdfDoc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
    }
}