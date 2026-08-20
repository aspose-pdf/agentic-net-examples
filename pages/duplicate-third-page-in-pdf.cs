using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages (pages are 1‑based)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than 3 pages.");
                return;
            }

            // Retrieve the third page
            Page thirdPage = doc.Pages[3];

            // Insert a copy of the third page immediately after it (position 4)
            // Insert(int, Page) creates a new page based on the supplied page.
            doc.Pages.Insert(4, thirdPage);

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 3 duplicated and inserted after the original. Saved to '{outputPath}'.");
    }
}