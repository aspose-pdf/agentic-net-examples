using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "portrait_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule for loading)
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (global rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Calculate current width and height from MediaBox
                double width  = page.MediaBox.URX - page.MediaBox.LLX;
                double height = page.MediaBox.URY - page.MediaBox.LLY;

                // If the page is landscape, swap dimensions to make it portrait
                if (width > height)
                {
                    // SetPageSize expects width then height; provide swapped values
                    page.SetPageSize(height, width);
                }
                // If already portrait, no change needed
            }

            // Save the modified document (using rule for saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages converted to portrait orientation. Saved to '{outputPath}'.");
    }
}