using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "uniform.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Find the largest width and height among all pages (1‑based indexing)
            double maxWidth = 0;
            double maxHeight = 0;

            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                var page = doc.Pages[i];
                if (page.PageInfo.Width > maxWidth)   maxWidth = page.PageInfo.Width;
                if (page.PageInfo.Height > maxHeight) maxHeight = page.PageInfo.Height;
            }

            // Resize every page to the maximum dimensions
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                var page = doc.Pages[i];
                page.PageInfo.Width = maxWidth;
                page.PageInfo.Height = maxHeight;
            }

            // Save the uniform PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Uniform PDF saved to '{outputPath}'.");
    }
}