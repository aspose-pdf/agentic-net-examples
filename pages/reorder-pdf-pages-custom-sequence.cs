using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reordered.pdf";

        // Define the desired page order (1‑based indexes).
        // Adjust this array to match the required sequence.
        int[] newOrder = new int[] { 3, 1, 2 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block (document‑disposal rule).
        using (Document src = new Document(inputPath))
        {
            // Validate that every index in the sequence is within the document range.
            foreach (int idx in newOrder)
            {
                if (idx < 1 || idx > src.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page index {idx}. Document contains {src.Pages.Count} pages.");
                    return;
                }
            }

            // Create an empty target PDF (also inside a using block).
            using (Document dst = new Document())
            {
                // Copy pages from the source to the target in the specified order.
                // PageCollection uses 1‑based indexing, so we can use the indices directly.
                foreach (int idx in newOrder)
                {
                    // Add the page from the source document to the destination.
                    // The page is moved from src to dst; src is no longer needed after this.
                    dst.Pages.Add(src.Pages[idx]);
                }

                // Save the reordered document (standard Save method).
                dst.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}