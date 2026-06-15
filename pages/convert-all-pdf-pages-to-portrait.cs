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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // MediaBox defines the page size (width x height)
                double width  = page.MediaBox.Width;
                double height = page.MediaBox.Height;

                // If the page is landscape (width > height), swap dimensions to make it portrait
                if (width > height)
                {
                    // SetPageSize expects width then height; swapping makes height > width
                    page.SetPageSize(height, width);
                }
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages converted to portrait orientation and saved to '{outputPath}'.");
    }
}