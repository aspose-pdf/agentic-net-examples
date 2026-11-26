using System;
using System.IO;
using Aspose.Pdf;   // All Aspose.Pdf types, including PptxSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "presentation.pdf";
        const string outputPath = "presentation_images.pptx";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure PPTX save options: render each slide as an image
                PptxSaveOptions pptxOpts = new PptxSaveOptions
                {
                    SlidesAsImages = true   // Enable slide‑as‑image conversion
                };

                // Save the PDF as a PPTX file using the explicit save options
                pdfDoc.Save(outputPath, pptxOpts);
            }

            Console.WriteLine($"Conversion completed: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}