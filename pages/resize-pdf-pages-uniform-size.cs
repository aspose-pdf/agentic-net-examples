using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "uniform_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            double maxWidth  = 0;
            double maxHeight = 0;

            // Determine the largest page dimensions (pages are 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Rectangle coordinates: LLX, LLY, URX, URY
                double width  = page.Rect.URX - page.Rect.LLX;
                double height = page.Rect.URY - page.Rect.LLY;

                if (width  > maxWidth)  maxWidth  = width;
                if (height > maxHeight) maxHeight = height;
            }

            // Resize every page to the largest dimensions
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.SetPageSize(maxWidth, maxHeight);
                // Alternatively: page.Resize(new PageSize((float)maxWidth, (float)maxHeight));
            }

            // Save the modified document (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages resized and saved to '{outputPath}'.");
    }
}