using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialize PPTX save options and enable raster‑image slides
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true
            };

            // Save the document as PPTX using the configured options
            pdfDoc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}