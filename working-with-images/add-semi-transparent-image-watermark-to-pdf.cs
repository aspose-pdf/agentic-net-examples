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

        // Opacity can be read from a config file; here we use a hard‑coded example.
        double opacity = 0.3; // 30 % opacity (range 0.0‑1.0)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a single ImageStamp instance; reuse it for every page.
            ImageStamp stamp = new ImageStamp(watermarkImagePath);
            stamp.Opacity = opacity;                 // set semi‑transparent opacity
            stamp.Background = false;                // place stamp on top of page content
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to each page in the document.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}