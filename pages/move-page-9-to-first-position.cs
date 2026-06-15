using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 9 pages
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document contains fewer than 9 pages.");
                return;
            }

            // Move page 9 to the first position.
            // PageCollection does not have a Move method, so we insert the page at the target index
            // and then delete its original occurrence.
            const int sourceIndex = 9; // 1‑based index of the page to move
            const int targetIndex = 1; // 1‑based index where the page should be placed

            // Retrieve the page to move
            Page pageToMove = doc.Pages[sourceIndex];

            // Insert the page at the target position
            doc.Pages.Insert(targetIndex, pageToMove);

            // Determine the index of the original page after insertion and delete it
            int deleteIndex = sourceIndex >= targetIndex ? sourceIndex + 1 : sourceIndex;
            doc.Pages.Delete(deleteIndex);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 9 moved to first position. Saved as '{outputPath}'.");
    }
}
