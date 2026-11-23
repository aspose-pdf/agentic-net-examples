using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "watermarked.pdf";    // result PDF
        const string imagePath  = "watermark.png";      // watermark image

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {imagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Apply the background image to each page (or select a specific page)
            foreach (Page page in doc.Pages)
            {
                // Create an ImageStamp from the image file
                ImageStamp stamp = new ImageStamp(imagePath)
                {
                    // Place the image behind existing page content
                    Background = true,

                    // Optional visual tweaks
                    Opacity = 0.3,                                 // semi‑transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,

                    // Stretch the image to cover the whole page
                    Width  = page.PageInfo.Width,
                    Height = page.PageInfo.Height
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF; Document.Save(string) writes PDF regardless of extension
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}