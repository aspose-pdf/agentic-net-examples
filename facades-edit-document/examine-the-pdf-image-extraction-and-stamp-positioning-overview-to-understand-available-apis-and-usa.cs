using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for extraction and stamping
using Aspose.Pdf;           // Core PDF types (if needed)

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // Source PDF
        const string outputPdf = "stamped_output.pdf";     // Result PDF with stamp
        const string stampImage = "stamp.png";             // Image to use as stamp
        const string imagesOutputDir = "ExtractedImages";  // Folder for extracted images

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the directory for extracted images exists
        Directory.CreateDirectory(imagesOutputDir);

        // ------------------------------------------------------------
        // 1. Extract all images from the PDF using PdfExtractor
        // ------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Configure the extractor to pull images only
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all images in the document
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(imagesOutputDir, $"image_{imageIndex}.png");
                // Save the current image to a file (default format is PNG)
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Extracted image {imageIndex} → {imagePath}");
                imageIndex++;
            }
        }

        // ------------------------------------------------------------
        // 2. Add an image stamp to every page using PdfFileStamp
        // ------------------------------------------------------------
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Initialize the facade with the source PDF
            fileStamp.BindPdf(inputPdf);

            // Create a Stamp object from the Facades namespace and bind the image
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(stampImage);

            // Configure visual appearance of the stamp
            stamp.Opacity = 0.5f;          // 50 % transparency
            stamp.SetOrigin(100, 150);    // Position from the bottom‑left corner (X=100, Y=150)
            stamp.SetImageSize(80, 80);   // Width = 80 pt, Height = 80 pt
            stamp.IsBackground = false;   // Place stamp on top of page content

            // Apply the stamp to the entire document (all pages)
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF
            fileStamp.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
