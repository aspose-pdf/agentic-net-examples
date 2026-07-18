using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkImage = "watermark.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(watermarkImage))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImage}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Apply the watermark to every page
            foreach (Page page in doc.Pages)
            {
                // Create an image stamp that will be used as a background watermark
                ImageStamp stamp = new ImageStamp(watermarkImage)
                {
                    Background = true,                     // place behind page content
                    Opacity = 0.3f,                        // semi‑transparent
                    Width = page.PageInfo.Width,            // full page width
                    Height = page.PageInfo.Height,          // full page height
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}