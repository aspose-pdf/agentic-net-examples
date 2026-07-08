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

        // Load the PDF document (using rule for document disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document must contain at least two pages.");
                return;
            }

            // Retrieve dimensions of page 2 (pages are 1‑based)
            double width = doc.Pages[2].PageInfo.Width;
            double height = doc.Pages[2].PageInfo.Height;

            // Insert a new empty page at position 5
            // (Insert returns the newly created Page object)
            Page newPage = doc.Pages.Insert(5);

            // Apply the same size as page 2
            newPage.SetPageSize(width, height);

            // Save the modified document (using rule for saving)
            doc.Save(outputPath);

            Console.WriteLine($"New page inserted at position 5 with size {width}x{height} and saved to '{outputPath}'.");
        }
    }
}
