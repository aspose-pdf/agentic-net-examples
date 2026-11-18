using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkImagePath = "watermark.png";

        // Configuration: opacity between 0 (fully transparent) and 1 (fully opaque)
        double opacity = 0.3; // semi‑transparent watermark

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Load PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Create an ImageStamp for the watermark
            ImageStamp stamp = new ImageStamp(watermarkImagePath)
            {
                Opacity = opacity,                         // set opacity from config
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                Background = false                         // draw on top of page content
            };

            // Apply the stamp to every page (one‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)   // page-indexing-one-based rule
            {
                Page page = doc.Pages[i];
                page.AddStamp(stamp);                     // add per page, not on collection
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}