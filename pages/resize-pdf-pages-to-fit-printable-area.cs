using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Target printable area in points (A4 size: 595 × 842)
            const double targetWidth  = 595.0;
            const double targetHeight = 842.0;

            // Iterate pages (1‑based indexing per rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page dimensions
                double origWidth  = page.PageInfo.Width;
                double origHeight = page.PageInfo.Height;

                // Compute uniform scaling factor to fit within target area
                double scale = Math.Min(targetWidth / origWidth, targetHeight / origHeight);

                // New size after scaling
                float newWidth  = (float)(origWidth  * scale);
                float newHeight = (float)(origHeight * scale);

                // Resize the page – this also scales the page content proportionally
                page.Resize(new Aspose.Pdf.PageSize(newWidth, newHeight));
            }

            // Save the modified PDF (standard PDF save, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}