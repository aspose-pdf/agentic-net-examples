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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than three pages.");
                return;
            }

            // Retrieve page 3 (1‑based indexing)
            Page page3 = doc.Pages[3];

            // Create an array containing the page to be duplicated
            Page[] pageToCopy = new Page[] { page3 };

            // Insert the copied page at position 4 (immediately after the original page 3)
            doc.Pages.CopyTo(pageToCopy, 4);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 3 duplicated and inserted after the original. Saved to '{outputPath}'.");
    }
}