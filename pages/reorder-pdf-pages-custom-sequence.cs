using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output PDF path (pages reordered)
        const string outputPath = "reordered.pdf";

        // Define the new page order (1‑based indexes).
        // Example: {3,1,2} will place original page 3 first, then page 1, then page 2.
        int[] newSequence = new int[] { 3, 1, 2 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source document.
        using (Document sourceDoc = new Document(inputPath))
        {
            // Create an empty target document.
            using (Document targetDoc = new Document())
            {
                // Ensure the source document has enough pages for the requested sequence.
                int pageCount = sourceDoc.Pages.Count;
                foreach (int idx in newSequence)
                {
                    if (idx < 1 || idx > pageCount)
                    {
                        Console.Error.WriteLine($"Invalid page index {idx} for source document with {pageCount} pages.");
                        return;
                    }
                }

                // Copy pages to the target document in the specified order.
                foreach (int srcPageNumber in newSequence)
                {
                    // Pages collection uses 1‑based indexing.
                    Page pageToCopy = sourceDoc.Pages[srcPageNumber];
                    // Add the page to the target document; this creates a copy.
                    targetDoc.Pages.Add(pageToCopy);
                }

                // Save the reordered PDF.
                targetDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}