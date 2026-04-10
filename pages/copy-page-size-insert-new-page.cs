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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document must contain at least two pages.");
                return;
            }

            // Get dimensions of page 2 (pages are 1‑based)
            Page sourcePage = doc.Pages[2];
            double width  = sourcePage.PageInfo.Width;
            double height = sourcePage.PageInfo.Height;

            // Insert a new empty page at position 5
            // Insert returns the newly created page instance
            Page newPage = doc.Pages.Insert(5);

            // Set the size of the new page to match page 2
            newPage.SetPageSize(width, height);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"New page inserted at position 5 with size of page 2. Saved to '{outputPath}'.");
    }
}