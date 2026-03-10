using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Reorder pages of a PDF according to the sequence provided in newOrder.
    // newOrder contains the original page numbers (1‑based) in the desired order.
    static void ReorderPdfPages(string inputPath, string outputPath, int[] newOrder)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source document.
        using (Document srcDoc = new Document(inputPath))
        {
            // Validate newOrder values.
            int pageCount = srcDoc.Pages.Count;
            foreach (int idx in newOrder)
            {
                if (idx < 1 || idx > pageCount)
                {
                    Console.Error.WriteLine($"Invalid page index {idx} in newOrder. Document has {pageCount} pages.");
                    return;
                }
            }

            // Create a new empty document to hold pages in the new order.
            using (Document reorderedDoc = new Document())
            {
                // Ensure the new document has at least one page (Aspose.Pdf requires a page to exist before adding others).
                // Add a temporary blank page which will be removed later if not needed.
                reorderedDoc.Pages.Add();

                // Add pages from the source document in the specified order.
                foreach (int srcPageNumber in newOrder)
                {
                    // The Add method copies the page content; the source page remains unchanged.
                    reorderedDoc.Pages.Add(srcDoc.Pages[srcPageNumber]);
                }

                // Remove the initially added blank page if the source had at least one page.
                if (reorderedDoc.Pages.Count > newOrder.Length)
                {
                    reorderedDoc.Pages.Delete(1); // first page is the placeholder.
                }

                // Save the reordered PDF.
                reorderedDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }

    static void Main()
    {
        // Example usage:
        // Input PDF with 5 pages. Desired order: 3,1,5,2,4
        const string inputPdf = "input.pdf";
        const string outputPdf = "reordered.pdf";
        int[] newPageOrder = { 3, 1, 5, 2, 4 };

        ReorderPdfPages(inputPdf, outputPdf, newPageOrder);
    }
}