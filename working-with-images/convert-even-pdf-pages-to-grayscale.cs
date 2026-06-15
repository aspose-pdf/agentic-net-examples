using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Process only even‑numbered pages.
                if (i % 2 == 0)
                {
                    Page page = doc.Pages[i];
                    // Convert the entire page to grayscale.
                    // This effectively replaces all images on the page with their grayscale versions.
                    page.MakeGrayscale();
                }
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}