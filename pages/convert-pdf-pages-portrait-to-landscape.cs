using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Calculate current width and height from MediaBox
                double width  = page.MediaBox.URX - page.MediaBox.LLX;
                double height = page.MediaBox.URY - page.MediaBox.LLY;

                // Swap width and height to make the page landscape
                page.SetPageSize(height, width);
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages converted to landscape and saved to '{outputPath}'.");
    }
}