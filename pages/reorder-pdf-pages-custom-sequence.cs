using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output PDF path
        const string outputPath = "reordered.pdf";

        // Define the new page order (1‑based indexes)
        // Example: {3,1,2} will place page 3 first, then page 1, then page 2
        int[] newOrder = { 3, 1, 2 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load source document and create target document inside using blocks
        using (Document srcDoc = new Document(inputPath))
        using (Document dstDoc = new Document())
        {
            // Validate that the requested indexes are within the source page count
            foreach (int idx in newOrder)
            {
                if (idx < 1 || idx > srcDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page index {idx}. Document has {srcDoc.Pages.Count} pages.");
                    return;
                }
            }

            // Copy pages to the target document in the specified order
            foreach (int idx in newOrder)
            {
                // Pages collection is 1‑based, so we can use the index directly
                dstDoc.Pages.Add(srcDoc.Pages[idx]);
            }

            // Save the reordered PDF
            dstDoc.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}