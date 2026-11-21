using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Target printable area (A4 size) in points
        const double targetWidth  = 595.0;
        const double targetHeight = 842.0;

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page dimensions
                double origWidth  = page.PageInfo.Width;
                double origHeight = page.PageInfo.Height;

                // Compute uniform scale factor to fit within target area
                double scale = Math.Min(targetWidth / origWidth, targetHeight / origHeight);

                // Apply scaling by adjusting page size proportionally
                page.PageInfo.Width  = origWidth * scale;
                page.PageInfo.Height = origHeight * scale;

                // Center the scaled page within the target rectangle using MarginInfo
                double offsetX = (targetWidth  - page.PageInfo.Width)  / 2.0;
                double offsetY = (targetHeight - page.PageInfo.Height) / 2.0;
                page.PageInfo.Margin = new MarginInfo(offsetX, offsetX, offsetY, offsetY);
            }

            // Save the modified PDF (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
