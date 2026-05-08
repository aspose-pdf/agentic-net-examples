using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_moved.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least nine pages
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document contains fewer than 9 pages.");
                return;
            }

            // PageCollection uses 1‑based indexing.
            // Move page 9 to the first position (index 1) using Insert + Delete.
            int sourceIndex = 9;   // original position of the page to move
            int targetIndex = 1;   // desired new position

            // Retrieve the page to move
            Page pageToMove = doc.Pages[sourceIndex];

            // Insert the page at the target position
            doc.Pages.Insert(targetIndex, pageToMove);

            // After insertion, the original page shifts to a new index.
            // If the source index is greater than or equal to the target index,
            // the original page will now be at sourceIndex + 1.
            int deleteIndex = sourceIndex >= targetIndex ? sourceIndex + 1 : sourceIndex;
            doc.Pages.Delete(deleteIndex);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 9 has been moved to the first position. Saved as '{outputPath}'.");
    }
}
