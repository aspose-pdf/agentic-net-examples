using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "minutes.pdf";
        const string outputPath = "minutes_images.pptx";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and convert it to PPTX with each slide rendered as an image
        using (Document doc = new Document(inputPath))
        {
            // Configure PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true   // Render each slide as a raster image
            };

            // Save the document as PPTX using the specified options
            doc.Save(outputPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputPath}'");
    }
}