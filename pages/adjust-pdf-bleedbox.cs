using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_adjusted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the recommended using pattern
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve the current BleedBox
                Aspose.Pdf.Rectangle currentBleed = page.BleedBox;

                // Example adjustment: expand each side by 5 points for printer bleed
                double expand = 5.0;
                Aspose.Pdf.Rectangle adjustedBleed = new Aspose.Pdf.Rectangle(
                    currentBleed.LLX - expand,
                    currentBleed.LLY - expand,
                    currentBleed.URX + expand,
                    currentBleed.URY + expand);

                // Apply the adjusted BleedBox back to the page
                page.BleedBox = adjustedBleed;
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Adjusted PDF saved to '{outputPath}'.");
    }
}