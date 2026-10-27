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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Set up PPTX save options to render each slide as a raster image
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    SlidesAsImages = true
                };

                // Save the PDF as a PPTX file using the explicit save options
                doc.Save(outputPath, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}