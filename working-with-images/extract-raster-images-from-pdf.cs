using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;

class ExtractImages
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output directory for extracted PNG images
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                int imageCounter = 1;

                // Iterate over all XImage resources on the current page
                foreach (XImage xImg in page.Resources.Images)
                {
                    // Build a unique file name for each image
                    string imagePath = Path.Combine(
                        outputDir,
                        $"page{pageNum}_image{imageCounter}.png");

                    // XImage.Save only accepts a Stream, so we first write the image to a MemoryStream
                    // and then re‑encode it as PNG using System.Drawing.
                    using (MemoryStream tempStream = new MemoryStream())
                    {
                        // Save the raw image data to the temporary stream
                        xImg.Save(tempStream);
                        tempStream.Position = 0; // rewind

                        // Load the image with System.Drawing and re‑save as PNG
                        using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(tempStream))
                        {
                            sysImg.Save(imagePath, ImageFormat.Png);
                        }
                    }

                    Console.WriteLine($"Saved image: {imagePath}");
                    imageCounter++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
