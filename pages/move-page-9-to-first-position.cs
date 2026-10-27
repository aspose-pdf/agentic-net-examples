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
            // Verify the document has at least 9 pages
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("Document contains fewer than 9 pages; cannot perform move operation.");
                return;
            }

            // Move page 9 to the first position using Insert/Delete (PageCollection has no Move method)
            const int sourceIndex = 9; // 1‑based index of the page to move
            const int targetIndex = 1; // 1‑based index where the page should be placed

            // Keep a reference to the page that will be moved
            Page pageToMove = doc.Pages[sourceIndex];

            // Insert the page at the target position
            doc.Pages.Insert(targetIndex, pageToMove);

            // After insertion the original page shifts; calculate its new index for deletion
            int deleteIndex = sourceIndex >= targetIndex ? sourceIndex + 1 : sourceIndex;

            // Delete the original occurrence of the page
            doc.Pages.Delete(deleteIndex);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 9 moved to first position. Saved as '{outputPath}'.");
    }
}
