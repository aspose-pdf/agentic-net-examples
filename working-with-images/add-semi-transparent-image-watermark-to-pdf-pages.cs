using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, watermark image and output PDF paths
        const string inputPdfPath   = "input.pdf";
        const string watermarkPath  = "watermark.png";
        const string outputPdfPath  = "output_watermarked.pdf";

        // Opacity can be read from a config source; here we use a hard‑coded value for illustration
        double opacity = 0.35; // Range 0.0 (fully transparent) to 1.0 (fully opaque)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the watermark image
            ImageStamp imgStamp = new ImageStamp(watermarkPath)
            {
                // Set the desired opacity (0.0 – 1.0)
                Opacity = opacity,

                // Optional: position the stamp (centered on each page)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Optional: scale the watermark (e.g., 50% of its original size)
                // Width  = imgStamp.Width  * 0.5,
                // Height = imgStamp.Height * 0.5
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}