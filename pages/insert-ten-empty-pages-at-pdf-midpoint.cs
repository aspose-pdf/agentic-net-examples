using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_inserted_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Calculate the position just after the middle of the existing pages.
            // For even counts this inserts after the first half; for odd counts it inserts after the exact middle page.
            int middlePosition = (doc.Pages.Count / 2) + 1;

            // Insert ten empty pages at the calculated position.
            // Insert(int) creates an empty page with the most common size in the document.
            for (int i = 0; i < 10; i++)
            {
                doc.Pages.Insert(middlePosition);
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with ten inserted pages at the midpoint: {outputPath}");
    }
}