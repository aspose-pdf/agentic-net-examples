using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string imagesFolder = "ExtractedImages";
        const string manifestPath = "ImageManifest.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(imagesFolder);

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(pdfPath);

            // The ExtractImageMode property is not available in this version of Aspose.Pdf.Facades.
            // Images are extracted by default when calling ExtractImage().
            extractor.ExtractImage();

            int index = 1;
            using (StreamWriter manifest = new StreamWriter(manifestPath, false))
            {
                // Iterate through all extracted images
                while (extractor.HasNextImage())
                {
                    string imageFileName = $"image{index}.png"; // you can change the extension / format as needed
                    string outPath = Path.Combine(imagesFolder, imageFileName);

                    // Save the image – here we choose PNG format via ImageFormat
                    extractor.GetNextImage(outPath, ImageFormat.Png);

                    // Write entry to manifest: "image1.png: PNG"
                    manifest.WriteLine($"{imageFileName}: PNG");
                    index++;
                }
            }

            int imageCount = index - 1;
            Console.WriteLine($"Extracted {imageCount} images to '{imagesFolder}'.");
            Console.WriteLine($"Image manifest written to '{manifestPath}'.");
        }
    }
}
