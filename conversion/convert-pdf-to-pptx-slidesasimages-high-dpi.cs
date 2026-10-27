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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure PPTX save options: render each slide as an image
                // and set a high resolution (DPI) for the rasterized images
                var pptxOptions = new PptxSaveOptions
                {
                    SlidesAsImages = true,
                    // The property that controls image resolution for PPTX conversion
                    // is ImageResolution (DPI). Adjust as needed for high‑quality output.
                    ImageResolution = 300
                };

                // Save the PDF as PPTX using the specified options
                pdfDoc.Save(outputPath, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
