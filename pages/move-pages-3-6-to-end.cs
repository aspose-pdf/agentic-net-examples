using System;
using System.IO;
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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 6 pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("Document does not contain enough pages to perform the operation.");
                return;
            }

            // Collect pages 3 through 6 (1‑based indexing)
            Page[] pagesToMove = new Page[4];
            for (int i = 0; i < 4; i++)
                pagesToMove[i] = doc.Pages[3 + i]; // pages 3,4,5,6

            // Insert the collected pages at the end of the document
            // Insert position is Count+1 because Insert expects a 1‑based index
            doc.Pages.Insert(doc.Pages.Count + 1, pagesToMove);

            // Delete the original pages 3‑6 (they are now duplicated at the end)
            // Deleting after insertion ensures the newly inserted pages stay at the end
            doc.Pages.Delete(new int[] { 3, 4, 5, 6 });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 moved to the end. Output saved to '{outputPath}'.");
    }
}