using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Resolve paths relative to the executable location for robustness
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputPdf = Path.Combine(baseDir, "sample.pdf");          // source PDF
        string outputPdf = Path.Combine(baseDir, "sample_stamped.pdf"); // PDF after stamping
        string stampImg = Path.Combine(baseDir, "logo.png");           // image to use as stamp
        string imagesDir = Path.Combine(baseDir, "ExtractedImages");    // folder for extracted images

        // ---------------------------------------------------------------------
        // Validate required files exist before proceeding (pre‑empt FileNotFound)
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdf}'.");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Error: Stamp image not found at '{stampImg}'.");
            return;
        }

        // ------------------------------------------------------------
        // 1. Extract all images from the PDF using PdfExtractor
        // ------------------------------------------------------------
        Directory.CreateDirectory(imagesDir);

        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Image extraction defaults to "all images"
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Save each image; format is inferred from the file extension
                string imagePath = Path.Combine(imagesDir, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Extracted image saved to: {imagePath}");
                imageIndex++;
            }
        }

        // ------------------------------------------------------------
        // 2. Add an image stamp to every page of the PDF using PdfFileStamp
        // ------------------------------------------------------------
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Initialize the facade with the source PDF (new API)
            fileStamp.BindPdf(inputPdf);

            // Create a stamp object and bind the image
            Stamp stamp = new Stamp();
            stamp.BindImage(stampImg);

            // Position the stamp (X, Y) measured from the lower‑left corner
            stamp.SetOrigin(100f, 500f);   // adjust as needed

            // Set the size of the stamp (width, height)
            stamp.SetImageSize(150f, 50f); // adjust as needed

            // Make the stamp semi‑transparent and place it as a background
            stamp.Opacity = 0.6f;
            stamp.IsBackground = true;

            // Add the stamp to the document (applies to all pages by default)
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF (new API)
            fileStamp.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to: {outputPdf}");
    }
}
