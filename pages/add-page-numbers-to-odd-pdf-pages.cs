using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "odd_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp. Default format is "#" which will be replaced by the page number.
            PageNumberStamp stamp = new PageNumberStamp();
            // Position the stamp at the bottom‑center of each page.
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Bottom;
            stamp.BottomMargin        = 20; // distance from the bottom edge

            // Iterate over all pages (1‑based indexing) and add the stamp only to odd pages.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                if (i % 2 == 1) // odd page
                {
                    Page page = doc.Pages[i];
                    page.AddStamp(stamp);
                }
            }

            // Save the modified PDF (lifecycle rule: use the same Document instance)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Odd‑page numbers added. Output saved to '{outputPath}'.");
    }
}