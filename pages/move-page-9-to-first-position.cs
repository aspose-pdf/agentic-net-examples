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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least nine pages
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document contains fewer than 9 pages.");
                return;
            }

            // Move page 9 to the first position (1‑based indexing)
            int sourceIndex = 9;   // original position of the page to move
            int targetIndex = 1;   // desired new position

            // Retrieve the page reference
            Page pageToMove = doc.Pages[sourceIndex];

            // Insert the page at the target position
            doc.Pages.Insert(targetIndex, pageToMove);

            // Delete the original occurrence of the page.
            // After insertion, the original page shifts one position forward if source >= target.
            int deleteIndex = sourceIndex >= targetIndex ? sourceIndex + 1 : sourceIndex;
            doc.Pages.Delete(deleteIndex);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 9 moved to first position. Saved as '{outputPath}'.");
    }
}
