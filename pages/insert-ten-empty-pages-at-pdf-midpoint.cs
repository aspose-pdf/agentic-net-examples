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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing (see global rule)
            int pageCount = doc.Pages.Count;

            // Calculate the middle index where new pages will be inserted.
            // For an even number of pages we insert after the first half.
            // Adding 1 makes the insertion point the first page of the second half.
            int middleIndex = (pageCount / 2) + 1;

            // Insert ten empty pages at the calculated position.
            // Use the overload that creates a blank page automatically.
            for (int i = 0; i < 10; i++)
            {
                doc.Pages.Insert(middleIndex);
            }

            // Save the modified document. The using block ensures the Document stays alive until Save completes.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted 10 empty pages at the midpoint. Saved to '{outputPath}'.");
    }
}
