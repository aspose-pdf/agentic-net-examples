using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Move pages 3‑6 to the end while preserving their original order.
            int firstPageToMove = 3;          // inclusive, 1‑based
            int pagesToMove = 4;              // pages 3,4,5,6

            for (int i = 0; i < pagesToMove; i++)
            {
                // The page currently at index 'firstPageToMove' is the next one to relocate.
                Page page = doc.Pages[firstPageToMove];

                // Insert the page just after the current last page (Count + 1 means "append").
                doc.Pages.Insert(doc.Pages.Count + 1, page);

                // Delete the original occurrence of the page.
                // After insertion the original page is still at 'firstPageToMove'.
                doc.Pages.Delete(firstPageToMove);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 moved to the end. Saved as '{outputPath}'.");
    }
}
