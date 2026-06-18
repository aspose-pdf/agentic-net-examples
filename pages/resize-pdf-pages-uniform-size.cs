using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "uniform_pages.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Variables must be declared outside the using block so they are in scope for the final message
        double maxWidth = 0;
        double maxHeight = 0;

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // First pass: find the maximum width and height among all pages
            for (int i = 1; i <= doc.Pages.Count; i++) // page indexing is 1‑based
            {
                Page page = doc.Pages[i];
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = page.Rect;
                double width = rect.URX - rect.LLX;
                double height = rect.URY - rect.LLY;

                if (width > maxWidth) maxWidth = width;
                if (height > maxHeight) maxHeight = height;
            }

            // Second pass: resize every page to the largest dimensions
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // SetPageSize resizes the page to the specified width and height
                page.SetPageSize(maxWidth, maxHeight);
                // Alternative using Resize (commented out):
                // page.Resize(new PageSize((float)maxWidth, (float)maxHeight));
            }

            // Save the modified document (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to {maxWidth}×{maxHeight} and saved to '{outputPath}'.");
    }
}
