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
            // Verify that the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document must contain at least two pages.");
                return;
            }

            // Retrieve dimensions of page 2 (1‑based indexing)
            Page pageTwo = doc.Pages[2];
            double width = pageTwo.PageInfo.Width;
            double height = pageTwo.PageInfo.Height;

            // Insert a new empty page at position 5
            Page newPage = doc.Pages.Insert(5);

            // Set the new page size to match page 2
            newPage.SetPageSize(width, height);

            // Save the modified document
            doc.Save(outputPath);

            // Report success while still inside the using block where width/height are in scope
            Console.WriteLine($"Inserted new page at position 5 with size {width}x{height} and saved to '{outputPath}'.");
        }
    }
}
