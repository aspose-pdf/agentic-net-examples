using System;
using System.IO;
using Aspose.Pdf; // Document, Page, Rectangle

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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the amount (in points) to adjust each side of the BleedBox.
            // 1 point = 1/72 inch. Adjust as needed for printer specifications.
            const double margin = 5.0;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve the current BleedBox. If it is null, use the MediaBox as a fallback.
                Rectangle currentBleed = page.BleedBox ?? page.MediaBox;

                // Adjust the rectangle by expanding each side by the specified margin.
                double newLlx = currentBleed.LLX - margin;
                double newLly = currentBleed.LLY - margin;
                double newUrx = currentBleed.URX + margin;
                double newUry = currentBleed.URY + margin;

                // Assign the adjusted BleedBox back to the page.
                page.BleedBox = new Rectangle(newLlx, newLly, newUrx, newUry);
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Adjusted PDF saved to '{outputPath}'.");
    }
}