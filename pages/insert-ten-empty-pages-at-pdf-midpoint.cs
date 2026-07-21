using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Calculate the middle position (1‑based indexing)
            // For an even number of pages, the new pages will be inserted after the first half
            int middlePosition = (doc.Pages.Count / 2) + 1;

            // Insert ten empty pages at the calculated midpoint
            for (int i = 0; i < 10; i++)
            {
                doc.Pages.Insert(middlePosition);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted 10 pages at the midpoint. Saved to '{outputPath}'.");
    }
}