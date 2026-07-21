using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and watermark image paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_watermarked.pdf";
        const string watermarkImagePath = "watermark.png";

        // Opacity value from configuration (range 0.0 to 1.0)
        double opacity = 0.35; // example value; replace with actual config read

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the watermark image
            ImageStamp stamp = new ImageStamp(watermarkImagePath)
            {
                // Set semi‑transparent opacity
                Opacity = opacity,
                // Position the stamp (centered on each page)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Ensure the stamp is placed on top of page content
                Background = false
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}