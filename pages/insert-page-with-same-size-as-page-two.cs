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

        // Load the PDF document (using rule for document disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document must contain at least two pages.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based
            Page sourcePage = doc.Pages[2];

            // Retrieve the size of page 2
            double width  = sourcePage.PageInfo.Width;
            double height = sourcePage.PageInfo.Height;

            // Insert a new empty page at position 5
            // (Insert uses 1‑based indexing as well)
            Page newPage = doc.Pages.Insert(5);

            // Apply the same dimensions as page 2
            newPage.SetPageSize(width, height);

            // Save the modified document (using rule for saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"New page inserted at position 5 with size {outputPath}.");
    }
}