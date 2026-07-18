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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the current page width (points)
                double currentWidth = page.PageInfo.Width;

                // If the page is wider than 600 points, resize it to A4
                if (currentWidth > 600)
                {
                    // PageSize.A4 provides the standard A4 dimensions
                    page.SetPageSize(
                        Aspose.Pdf.PageSize.A4.Width,
                        Aspose.Pdf.PageSize.A4.Height);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}