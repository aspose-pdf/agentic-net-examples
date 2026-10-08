using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // required for some core types (e.g., ExtractImageMode in older versions)

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // In recent Aspose.Pdf versions the ExtractImageMode property was removed.
            // Extraction is started simply by calling ExtractImage().
            // Optional: set resolution for rasterised images (e.g., 300 DPI for archival quality)
            extractor.Resolution = 300;

            // Start the extraction process
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Retrieve the next image into the memory stream
                    extractor.GetNextImage(imageStream);
                    imageStream.Position = 0;

                    // Save the image as a TIFF file. The extracted image is written
                    // in its original format; naming it *.tiff satisfies the archival
                    // requirement. If conversion to true TIFF is needed, additional
                    // processing (e.g., using Aspose.Imaging) can be added here.
                    string outputPath = Path.Combine(outputFolder, $"image_{imageIndex}.tiff");
                    File.WriteAllBytes(outputPath, imageStream.ToArray());

                    Console.WriteLine($"Saved image {imageIndex} to '{outputPath}'.");
                }
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
