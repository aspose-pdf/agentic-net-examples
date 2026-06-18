using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "even_pages_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Loop through pages using 1‑based indexing; process only even pages
            for (int i = 2; i <= doc.Pages.Count; i += 2)
            {
                Page page = doc.Pages[i];

                // Create a page number stamp for the current even page
                PageNumberStamp stamp = new PageNumberStamp()
                {
                    // Center the number at the bottom of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    // Optional: adjust margins if needed
                    BottomMargin = 20
                };

                // Add the stamp to the page
                page.AddStamp(stamp);
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Even‑page numbers added. Output saved to '{outputPath}'.");
    }
}