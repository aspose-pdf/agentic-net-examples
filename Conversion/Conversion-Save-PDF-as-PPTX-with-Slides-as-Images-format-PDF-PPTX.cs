using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pptx";

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
                // Configure PPTX save options to render each slide as an image
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    SlidesAsImages = true
                };

                // Save the PDF as PPTX using the specified options
                pdfDoc.Save(outputPath, pptxOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}