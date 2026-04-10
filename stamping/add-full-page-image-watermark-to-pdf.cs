using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // for ImageStamp, HorizontalAlignment, VerticalAlignment

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string imagePath  = "watermark.png";   // image to use as background

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp that will act as a full‑page background watermark
                ImageStamp stamp = new ImageStamp(imagePath)
                {
                    // Place the stamp behind the page content
                    Background = true,

                    // Make the stamp cover the whole page
                    Width  = page.Rect.Width,
                    Height = page.Rect.Height,
                    XIndent = 0,
                    YIndent = 0,

                    // Center alignment (optional, ensures proper placement)
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,

                    // Adjust opacity as desired (0.0 = fully transparent, 1.0 = opaque)
                    Opacity = 0.3f
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}