using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_a5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // A5 size in points: 420 (width) x 595 (height)
                page.PageInfo.Width = 420;
                page.PageInfo.Height = 595;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to A5 and saved as '{outputPath}'.");
    }
}