using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "uniform_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Variables must be declared outside the using block if they are needed after it.
        double maxWidth = 0;
        double maxHeight = 0;

        // Load the PDF document (using rule: document must be wrapped in a using block)
        using (Document doc = new Document(inputPath))
        {
            // Determine the maximum width and height among all pages
            // Pages are 1‑based (rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Rectangle coordinates: LLX, LLY, URX, URY
                double width = page.Rect.URX - page.Rect.LLX;
                double height = page.Rect.URY - page.Rect.LLY;

                if (width > maxWidth) maxWidth = width;
                if (height > maxHeight) maxHeight = height;
            }

            // Resize every page to the largest dimensions
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Use SetPageSize – the correct API for resizing a page (width, height as doubles).
                page.SetPageSize(maxWidth, maxHeight);
            }

            // Save the modified document (rule: Document.Save writes PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to {maxWidth}x{maxHeight} and saved to '{outputPath}'.");
    }
}
