using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API and SaveOptions classes

class Program
{
    static void Main()
    {
        const string inputPath  = "brochure.pdf";
        const string outputPath = "brochure_images.pptx";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure PPTX save options – enable SlidesAsImages to render each slide as an image
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true
            };

            // Save the PDF as a PPTX file using the configured options
            pdfDoc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputPath}'");
    }
}