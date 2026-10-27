using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the reordered output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "reordered.pdf";

        // Define the new page order using 1‑based indices (e.g., {3,1,2})
        int[] newSequence = new int[] { 3, 1, 2 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source document and create an empty target document.
        // Both documents are wrapped in using blocks for deterministic disposal.
        using (Document src = new Document(inputPath))
        using (Document target = new Document())
        {
            // Validate that each index in the new sequence is within the valid range.
            foreach (int idx in newSequence)
            {
                if (idx < 1 || idx > src.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page index {idx}. Must be between 1 and {src.Pages.Count}.");
                    return;
                }
            }

            // Copy pages from the source to the target in the specified order.
            // Aspose.Pdf uses 1‑based page indexing.
            foreach (int idx in newSequence)
            {
                // Add a copy of the source page to the target document.
                target.Pages.Add(src.Pages[idx]);
            }

            // Save the reordered document while the target is still alive.
            target.Save(outputPath);
        }

        Console.WriteLine($"Pages reordered and saved to '{outputPath}'.");
    }
}