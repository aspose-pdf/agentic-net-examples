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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the original page count (1‑based indexing)
            int originalCount = doc.Pages.Count;

            // Calculate the insertion start index (middle of the document)
            // For even counts we insert after the first half; for odd counts after the middle page.
            int startIndex = (originalCount / 2) + 1; // 1‑based index

            // Insert ten empty pages sequentially so they appear together
            for (int i = 0; i < 10; i++)
            {
                doc.Pages.Insert(startIndex + i);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted 10 pages at the midpoint. Saved to '{outputPath}'.");
    }
}