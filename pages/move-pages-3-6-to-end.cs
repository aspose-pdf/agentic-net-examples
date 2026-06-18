using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that pages 3‑6 exist
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("Document must contain at least 6 pages.");
                return;
            }

            // Gather pages 3,4,5,6 into an array (pages are 1‑based)
            Page[] pagesToMove = new Page[4];
            for (int i = 0; i < 4; i++)
                pagesToMove[i] = doc.Pages[3 + i];

            // Insert the collected pages at the end of the document.
            // Insert position is Count+1 because Insert inserts *before* the given index.
            doc.Pages.Insert(doc.Pages.Count + 1, pagesToMove);

            // Remove the original pages 3‑6 now that they have been duplicated at the end.
            doc.Pages.Delete(new int[] { 3, 4, 5, 6 });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 moved to the end. Output saved to '{outputPath}'.");
    }
}