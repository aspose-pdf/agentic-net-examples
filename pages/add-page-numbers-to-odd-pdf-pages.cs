using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // not required but safe for other facades if needed

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
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                // Apply page number stamp only on odd pages
                if (pageNumber % 2 == 1)
                {
                    Page page = doc.Pages[pageNumber];

                    // Create a PageNumberStamp; default format "#" will be replaced by the page number
                    PageNumberStamp stamp = new PageNumberStamp();

                    // Optional: configure appearance and position
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                    stamp.BottomMargin        = 20; // distance from bottom edge

                    // Add the stamp to the current page
                    page.AddStamp(stamp);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Odd page numbers added and saved to '{outputPath}'.");
    }
}