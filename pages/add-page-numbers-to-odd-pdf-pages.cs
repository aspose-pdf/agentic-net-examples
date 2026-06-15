using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // For HorizontalAlignment and VerticalAlignment enums

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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Apply stamp only on odd‑numbered pages
                if (i % 2 == 1)
                {
                    // Create a page number stamp; default format is "#"
                    PageNumberStamp stamp = new PageNumberStamp();

                    // Position the stamp at the bottom‑center of the page
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                    stamp.BottomMargin        = 20; // distance from bottom edge

                    // Add the stamp to the current page
                    doc.Pages[i].AddStamp(stamp);
                }
            }

            // Save the modified document (PDF format is implicit)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Odd page numbers added. Output saved to '{outputPath}'.");
    }
}