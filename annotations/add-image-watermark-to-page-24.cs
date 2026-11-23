using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "watermark.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least 24 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 24)
            {
                Console.Error.WriteLine("The document contains fewer than 24 pages.");
                return;
            }

            // Get page 24
            Page page = doc.Pages[24];

            // Create an ImageStamp that will act as a watermark
            ImageStamp stamp = new ImageStamp(imagePath)
            {
                // Make the stamp cover the whole page
                Width = page.MediaBox.Width,
                Height = page.MediaBox.Height,
                // Set the desired opacity (30%)
                Opacity = 0.3,
                // Place the stamp behind the existing page content (optional, but typical for watermarks)
                Background = true
            };

            // Add the stamp to the page
            page.AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
