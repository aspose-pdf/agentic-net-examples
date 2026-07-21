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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original page dimensions (points)
                double originalWidth  = page.PageInfo.Width;
                double originalHeight = page.PageInfo.Height;

                // Desired fixed width
                const double targetWidth = 800.0;

                // Compute scaling factor to preserve aspect ratio
                double scale = targetWidth / originalWidth;
                double targetHeight = originalHeight * scale;

                // Apply the new size to the page
                page.SetPageSize(targetWidth, targetHeight);
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages resized to width 800 points and saved to '{outputPath}'.");
    }
}