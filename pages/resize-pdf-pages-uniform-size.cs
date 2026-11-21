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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            double maxWidth = 0;
            double maxHeight = 0;

            // Determine the largest page dimensions using PageInfo (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                if (page.PageInfo.Width > maxWidth)  maxWidth  = page.PageInfo.Width;
                if (page.PageInfo.Height > maxHeight) maxHeight = page.PageInfo.Height;
            }

            // Resize every page to the maximum width and height via PageInfo
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.PageInfo.Width  = maxWidth;
                page.PageInfo.Height = maxHeight;
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages resized and saved to '{outputPath}'.");
    }
}