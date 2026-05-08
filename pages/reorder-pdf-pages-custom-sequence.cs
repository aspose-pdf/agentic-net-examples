using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "reordered.pdf";

        // Define the new page order (1‑based indexes)
        int[] newOrder = { 3, 1, 2 }; // example: page 3 becomes first, then 1, then 2

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load source document (wrapped in using for deterministic disposal)
        using (Document srcDoc = new Document(inputPath))
        {
            // Copy all source pages into an array to avoid mutating the collection while reordering
            Page[] allPages = new Page[srcDoc.Pages.Count];
            srcDoc.Pages.CopyTo(allPages, 0);

            // Create a new empty document to hold the reordered pages
            using (Document dstDoc = new Document())
            {
                // Add pages to the new document according to the custom sequence
                foreach (int pageNumber in newOrder)
                {
                    // Validate the requested page number (pages are 1‑based)
                    if (pageNumber < 1 || pageNumber > allPages.Length)
                    {
                        Console.Error.WriteLine($"Invalid page index {pageNumber} – skipping.");
                        continue;
                    }

                    // Add the corresponding page (array is 0‑based, so subtract 1)
                    dstDoc.Pages.Add(allPages[pageNumber - 1]);
                }

                // Save the reordered PDF (using the standard Save method)
                dstDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}