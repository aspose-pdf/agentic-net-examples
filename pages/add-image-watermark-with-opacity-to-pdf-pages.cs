using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // not needed but harmless
using Aspose.Pdf.Facades;   // not needed but harmless

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkImagePath = "watermark.png";

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a single ImageStamp instance; it can be reused for all pages
            ImageStamp stamp = new ImageStamp(watermarkImagePath)
            {
                // Place the watermark in the center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Ensure the stamp is drawn on top of page content
                Background = false,
                // Set opacity to 20% for subtle branding
                Opacity = 0.2
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF; no SaveOptions needed for PDF output
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}