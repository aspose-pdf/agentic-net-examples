using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: wrap Document in using)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Determine page size
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Create a rectangle that spans the page diagonally.
            // We'll use a square that starts at (0,0) and ends at (pageWidth, pageHeight)
            // This will cover the whole page; the visual effect of a diagonal
            // watermark can be achieved by rotating the annotation later if needed.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, pageWidth, pageHeight);

            // Create the WatermarkAnnotation
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                // Set a semi‑transparent color (gradient not directly supported via API)
                Color   = Aspose.Pdf.Color.FromRgb(0.8, 0.8, 0.8), // light gray
                Opacity = 0.5, // 50% opacity
                // Optional: set contents (text) – can be styled via SetText if needed
                Contents = "CONFIDENTIAL"
            };

            // Add the annotation to the page
            page.Annotations.Add(watermark);

            // Save the modified PDF (using rule: Document.Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}