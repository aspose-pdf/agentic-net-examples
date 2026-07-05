using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // If the page width exceeds 600 points, resize it to A4
                if (page.PageInfo.Width > 600)
                {
                    // Set page size to A4 (static PageSize.A4 provides width/height)
                    page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
                }
            }

            // Save the modified document (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}