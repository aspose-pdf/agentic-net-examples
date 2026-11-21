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

        // Define the range to extract (inclusive) and the new insertion position.
        // All page indexes are 1‑based as required by Aspose.Pdf.
        int rangeStart   = 2;   // first page of the range
        int rangeEnd     = 4;   // last page of the range
        int newPosition  = 6;   // position where the modified pages will be inserted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source document inside a using block for deterministic disposal.
        using (Document srcDoc = new Document(inputPath))
        {
            // Collect the pages to be moved.
            List<Page> extractedPages = new List<Page>();
            for (int i = rangeStart; i <= rangeEnd; i++)
            {
                // Pages collection is 1‑based.
                extractedPages.Add(srcDoc.Pages[i]);
            }

            // Remove the original pages from the source document.
            // Deleting in reverse order prevents index shifting.
            for (int i = rangeEnd; i >= rangeStart; i--)
            {
                srcDoc.Pages.Delete(i);
            }

            // Change each extracted page size to A4.
            foreach (Page page in extractedPages)
            {
                // SetPageSize expects width and height values, not the PageSize enum.
                page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
            }

            // Re‑insert the modified pages at the desired position.
            // Insert inserts before the specified 1‑based index.
            int insertIndex = newPosition;
            foreach (Page page in extractedPages)
            {
                srcDoc.Pages.Insert(insertIndex, page);
                insertIndex++; // advance so pages keep their order
            }

            // Save the resulting document.
            srcDoc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
