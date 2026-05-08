using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based indexed in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // If the page width is greater than 600 points, resize it to A4
                if (page.PageInfo.Width > 600)
                {
                    // PageSize.A4 provides the standard A4 dimensions (≈595×842 points)
                    page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
                }
            }

            // Save the modified document (no extra SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}