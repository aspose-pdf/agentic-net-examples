using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file (will be created if it does not exist)
        const string inputPdf = "input.pdf";

        // Directory where extracted images will be saved
        const string imagesDir = "extracted_images";

        // Markdown file that will contain the gallery
        const string markdownFile = "gallery.md";

        // Ensure the images directory exists
        Directory.CreateDirectory(imagesDir);

        // ---------------------------------------------------------------------
        // Ensure a source PDF exists – create a simple one if it is missing.
        // This prevents the runtime FileNotFoundException shown in the original
        // build output and makes the sample self‑contained.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdf(inputPdf);
            Console.WriteLine($"Sample PDF created at '{inputPdf}'.");
        }

        // Extract images using PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // Perform the image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build the output image file name (e.g., image-1.jpg, image-2.jpg, ...)
                string imagePath = Path.Combine(imagesDir, $"image-{imageIndex}.jpg");

                // Save the next extracted image to the file (default format is JPEG)
                extractor.GetNextImage(imagePath);

                imageIndex++;
            }
        }

        // Generate Markdown gallery
        using (StreamWriter writer = new StreamWriter(markdownFile))
        {
            writer.WriteLine("# Image Gallery");
            writer.WriteLine();

            // List all extracted images sorted by name
            string[] imageFiles = Directory.GetFiles(imagesDir, "image-*.jpg");
            Array.Sort(imageFiles);

            foreach (string imageFile in imageFiles)
            {
                // Use relative path for the markdown link (forward slashes for markdown compatibility)
                string relativePath = Path.Combine(imagesDir, Path.GetFileName(imageFile)).Replace('\\', '/');
                writer.WriteLine($"![]({relativePath})");
                writer.WriteLine(); // Blank line between images
            }
        }

        Console.WriteLine($"Images extracted to '{imagesDir}' and markdown gallery created at '{markdownFile}'.");
    }

    /// <summary>
    /// Creates a very small PDF containing a single page with a sample image.
    /// The image is added programmatically so that the extractor has something to find.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a simple in‑memory bitmap (blue square) and add it as an ImageStamp.
            // This guarantees that the PDF contains an actual raster image that the
            // PdfExtractor can retrieve.
            using (Bitmap bmp = new Bitmap(100, 100))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.Blue);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Png);
                    ms.Position = 0;

                    ImageStamp imgStamp = new ImageStamp(ms)
                    {
                        // Set a reasonable size for the image on the page.
                        Width = 200,
                        Height = 200,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    // Corrected: use AddStamp instead of the non‑existent Add method.
                    page.AddStamp(imgStamp);
                }
            }

            // Save the document
            doc.Save(path);
        }
    }
}
