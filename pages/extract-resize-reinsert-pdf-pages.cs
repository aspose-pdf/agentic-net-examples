using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Define the range of pages to extract (inclusive, 1‑based indexing)
        int rangeStart = 2; // first page of the range
        int rangeEnd   = 4; // last page of the range

        // Define the position where the extracted pages will be re‑inserted
        // (1‑based index after which the pages will appear)
        int insertAfterPage = 5;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate range
            if (rangeStart < 1 || rangeEnd > doc.Pages.Count || rangeStart > rangeEnd)
            {
                Console.Error.WriteLine("Invalid page range.");
                return;
            }

            // Collect the pages to be moved.
            // Deleting from the end preserves correct indexing while removing.
            List<Page> extractedPages = new List<Page>();
            for (int i = rangeEnd; i >= rangeStart; i--)
            {
                Page page = doc.Pages[i];          // keep a reference before deletion
                extractedPages.Add(page);
                doc.Pages.Delete(i);               // remove from the document
            }

            // Reverse to restore original order (since we removed from end)
            extractedPages.Reverse();

            // Adjust the insertion index because pages have been removed.
            // If the insertion point was after the removed range, it shifts left.
            int insertionIndex = insertAfterPage;
            if (insertAfterPage > rangeEnd)
                insertionIndex -= (rangeEnd - rangeStart + 1);
            // Ensure the index is within the current collection bounds (+1 for Insert after)
            insertionIndex = Math.Max(1, Math.Min(insertionIndex + 1, doc.Pages.Count + 1));

            // Insert each extracted page at the new position, resizing to A4 (595 x 842 points)
            foreach (Page page in extractedPages)
            {
                page.PageInfo.Width  = 595; // A4 width in points
                page.PageInfo.Height = 842; // A4 height in points
                doc.Pages.Insert(insertionIndex, page);
                insertionIndex++; // subsequent pages go after the previously inserted one
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}