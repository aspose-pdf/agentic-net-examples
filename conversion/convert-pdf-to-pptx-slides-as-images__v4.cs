using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "proposal.pdf";
        const string outputPath = "proposal_images.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Enable conversion of each PDF page to an image on the PPTX slide
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    SlidesAsImages = true
                };

                // Save as PPTX using the specified options
                doc.Save(outputPath, pptxOptions);
            }

            Console.WriteLine($"Conversion completed: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}