using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Annotations;        // For possible annotation types (not used here)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string imagePath  = "watermark.png";   // Image to use as watermark
        const string outputPath = "watermarked.pdf";

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create an ImageStamp from the image file.
            // Opacity defaults to 1.0 (fully opaque), so we don't need to set it explicitly,
            // but we set it for clarity.
            ImageStamp imgStamp = new ImageStamp(imagePath)
            {
                Opacity = 1.0f,                     // Fully opaque
                Background = false,                 // Stamp appears on top of page content
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Optional: set margins or explicit coordinates if desired
                // XIndent = 0,
                // YIndent = 0,
                // Width  = 200,   // Example scaling
                // Height = 100
            };

            // Apply the stamp to every page (page collection is 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}