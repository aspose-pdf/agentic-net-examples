using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "contract.pdf";
        const string outputPath = "contract_images.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document pdfDoc = new Document(inputPath))
            {
                // Save as PPTX with each slide rendered as an image.
                PptxSaveOptions pptxOpts = new PptxSaveOptions
                {
                    SlidesAsImages = true
                };

                pdfDoc.Save(outputPath, pptxOpts);
            }

            Console.WriteLine($"Conversion successful: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}