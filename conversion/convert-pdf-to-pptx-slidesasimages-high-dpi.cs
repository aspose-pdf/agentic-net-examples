using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure PPTX save options:
                // - SlidesAsImages renders each slide as a raster image.
                // - ImageResolution sets the DPI of those raster images (higher = better quality).
                var pptxOpts = new PptxSaveOptions
                {
                    SlidesAsImages = true,
                    ImageResolution = 300 // DPI
                };

                // Save the PDF as a PPTX using the explicit options.
                pdfDoc.Save(outputPptx, pptxOpts);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
