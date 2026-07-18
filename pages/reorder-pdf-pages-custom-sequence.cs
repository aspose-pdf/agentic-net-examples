using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath  = "input.pdf";
        // Output PDF file path (reordered pages)
        const string outputPath = "reordered.pdf";

        // Define the new page order (1‑based indexes)
        // Example: {3, 1, 2} will place page 3 first, then page 1, then page 2
        int[] newOrder = new int[] { 3, 1, 2 };

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source document and create an empty target document
        using (Document srcDoc = new Document(inputPath))
        using (Document dstDoc = new Document())
        {
            // Ensure the requested order is within the source page count
            foreach (int pageNumber in newOrder)
            {
                if (pageNumber < 1 || pageNumber > srcDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for source document with {srcDoc.Pages.Count} pages.");
                    return;
                }

                // Add the page from the source document to the target document.
                // The Add method copies the page; the original document remains unchanged.
                dstDoc.Pages.Add(srcDoc.Pages[pageNumber]);
            }

            // Save the reordered PDF
            dstDoc.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}