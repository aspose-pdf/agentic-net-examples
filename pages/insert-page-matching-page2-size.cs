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

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the source document has at least two pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document must contain at least two pages.");
                return;
            }

            // Retrieve dimensions of page 2
            Page pageTwo = doc.Pages[2];
            double width  = pageTwo.PageInfo.Width;
            double height = pageTwo.PageInfo.Height;

            // Insert a blank page at position 5 (1‑based index). If the document has fewer than 5 pages, it will be added at the end.
            doc.Pages.Insert(5);

            // Get the newly inserted page and set its size to match page 2
            Page newPage = doc.Pages[5];
            newPage.PageInfo.Width  = width;
            newPage.PageInfo.Height = height;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted a new page at position 5. Saved to '{outputPath}'.");
    }
}
