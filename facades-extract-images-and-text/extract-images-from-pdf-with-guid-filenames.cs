using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // Prepare the extractor for image extraction
            extractor.ExtractImage();

            // Loop through all images found in the document
            while (extractor.HasNextImage())
            {
                // Create a unique filename using a GUID to avoid collisions
                string guid = Guid.NewGuid().ToString();
                string outPath = Path.Combine(outputDir, guid + ".png");

                // Save the current image as PNG
                extractor.GetNextImage(outPath, ImageFormat.Png);
            }
        }

        Console.WriteLine($"All images have been extracted to '{outputDir}'.");
    }
}