using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // 1‑based indexing: pages 3‑6 are at positions 3,4,5,6
            // Extract those pages into an array
            Page[] pagesToMove = doc.Pages
                                    .Skip(2)          // skip first two pages (0‑based for Skip)
                                    .Take(4)          // take pages 3‑6
                                    .ToArray();

            // Insert the extracted pages at the end of the collection
            // Insertion position is Count+1 because Insert expects a 1‑based index
            int insertPosition = doc.Pages.Count + 1;
            doc.Pages.Insert(insertPosition, pagesToMove);

            // Delete the original pages (3‑6) now that they have been copied to the end
            // Delete accepts an array of 1‑based page numbers
            int[] pagesToDelete = { 3, 4, 5, 6 };
            doc.Pages.Delete(pagesToDelete);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 moved to the end. Saved as '{outputPath}'.");
    }
}