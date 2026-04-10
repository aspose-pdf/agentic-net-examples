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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 9 pages
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document contains fewer than 9 pages.");
                return;
            }

            // Move page 9 to the first position using Insert/Delete (PageCollection has no Move method)
            int sourceIndex = 9;   // 1‑based index of the page to move
            int targetIndex = 1;   // 1‑based index where the page should be placed

            // Keep a reference to the page we want to move
            Page pageToMove = doc.Pages[sourceIndex];

            // Insert the page at the target position
            doc.Pages.Insert(targetIndex, pageToMove);

            // After insertion the original page shifts one position to the right if source > target
            // Calculate the index of the original page that now needs to be removed
            int deleteIndex = sourceIndex >= targetIndex ? sourceIndex + 1 : sourceIndex;
            doc.Pages.Delete(deleteIndex);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 9 moved to first position. Saved as '{outputPath}'.");
    }
}
