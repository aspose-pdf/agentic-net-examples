using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";          // input PDF
        const string extractedDir = "ExtractedImages"; // folder for extracted images
        const string imageToAdd = "newImage.jpg";       // image to embed
        const string outputPdf = "output_with_image.pdf";

        // -----------------------------------------------------------------
        // 1. Extract all images from the source PDF using PdfExtractor
        // -----------------------------------------------------------------
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {sourcePdf}");
            return;
        }

        Directory.CreateDirectory(extractedDir);

        try
        {
            // Create and bind the extractor to the PDF file
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(sourcePdf);

            // Prepare the extractor to pull images
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop while more images are available
            while (extractor.HasNextImage())
            {
                // Save each image as PNG (you can choose other formats)
                string outPath = Path.Combine(extractedDir, $"image_{imageIndex}.png");
                extractor.GetNextImage(outPath, ImageFormat.Png);
                Console.WriteLine($"Extracted image {imageIndex} -> {outPath}");
                imageIndex++;
            }

            // No need to call Close() on PdfExtractor; it releases resources automatically
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during image extraction: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Add a new image to the first page of the PDF using PdfFileMend
        // -----------------------------------------------------------------
        if (!File.Exists(imageToAdd))
        {
            Console.Error.WriteLine($"Image to add not found: {imageToAdd}");
            return;
        }

        try
        {
            // PdfFileMend works with an input PDF and writes to a new output PDF
            PdfFileMend mender = new PdfFileMend(sourcePdf, outputPdf);

            // Open the image file as a stream
            using (FileStream imgStream = File.OpenRead(imageToAdd))
            {
                // Add the image to page 1.
                // Coordinates are in points (1 point = 1/72 inch). Adjust as needed.
                // lower-left (10,10) to upper-right (200,200) defines the rectangle.
                bool added = mender.AddImage(imgStream, 1, 10f, 10f, 200f, 200f);
                if (added)
                {
                    Console.WriteLine($"Image added to page 1 of {outputPdf}");
                }
                else
                {
                    Console.WriteLine("Failed to add image.");
                }
            }

            // Persist changes and release resources
            mender.Close();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during image addition: {ex.Message}");
        }
    }
}