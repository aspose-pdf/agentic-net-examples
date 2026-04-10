using System;
using System.IO;
using System.Collections.Generic;
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
            // Ensure the document has at least 6 pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("Document does not contain enough pages.");
                return;
            }

            // Collect pages 3 through 6 (1‑based indexing)
            List<Page> pagesToMove = new List<Page>();
            for (int i = 3; i <= 6; i++)
                pagesToMove.Add(doc.Pages[i]);

            // Insert the collected pages at the end of the document
            // Insert position is Count+1 because Insert expects a 1‑based index
            doc.Pages.Insert(doc.Pages.Count + 1, pagesToMove);

            // Delete the original pages 3‑6
            // Delete accepts an array of page numbers (1‑based)
            doc.Pages.Delete(new int[] { 3, 4, 5, 6 });

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 moved to the end. Saved as '{outputPath}'.");
    }
}