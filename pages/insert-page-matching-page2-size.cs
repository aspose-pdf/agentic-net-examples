using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that there are at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document must contain at least two pages.");
                return;
            }

            // Retrieve width and height of page 2 (pages are 1‑based)
            Page sourcePage = doc.Pages[2];
            double width = sourcePage.PageInfo.Width;
            double height = sourcePage.PageInfo.Height;

            // Insert a new empty page at position 5
            // If the document has fewer than 5 pages, the page is added at the end
            Page newPage = doc.Pages.Insert(5);

            // Set the new page size to match page 2
            newPage.SetPageSize(width, height);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted page at position 5 with dimensions of page 2. Saved to '{outputPath}'.");
    }
}