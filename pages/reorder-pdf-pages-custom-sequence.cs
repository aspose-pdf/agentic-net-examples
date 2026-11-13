using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reordered.pdf";

        // Define the new page order (1‑based indices as required by Aspose.Pdf)
        int[] newOrder = { 3, 1, 2 }; // example: page 3 becomes first, then 1, then 2

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load source document and create an empty target document.
        // Both documents are wrapped in using blocks for deterministic disposal.
        using (Document src = new Document(inputPath))
        using (Document target = new Document())
        {
            // Iterate over the custom sequence and add pages to the target document.
            // Page indexing in Aspose.Pdf is 1‑based, so we use the values directly.
            foreach (int pageNumber in newOrder)
            {
                // Validate page number to avoid IndexOutOfRangeException.
                if (pageNumber < 1 || pageNumber > src.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} – skipping.");
                    continue;
                }

                // Add the specified page to the target document.
                // This copies the page without altering the source document.
                target.Pages.Add(src.Pages[pageNumber]);
            }

            // Save the reordered PDF. Save() is called inside the using block
            // so the target document remains alive until the operation completes.
            target.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}