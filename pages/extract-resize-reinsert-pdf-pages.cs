using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "output.pdf";     // result PDF

        // Define the range of pages to extract (1‑based inclusive)
        const int rangeStart = 2;
        const int rangeEnd   = 4;

        // Define the position where the extracted pages will be re‑inserted
        // (insert after this page number; 0 means insert at the very beginning)
        const int insertAfter = 6;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Collect the pages that belong to the requested range.
            // -----------------------------------------------------------------
            List<Page> extractedPages = new List<Page>();
            for (int i = rangeStart; i <= rangeEnd; i++)
            {
                // Pages collection is 1‑based.
                extractedPages.Add(doc.Pages[i]);
            }

            // -----------------------------------------------------------------
            // 2. Resize each extracted page to A4.
            //    Use PageInfo.Width / Height (the correct API for page size).
            // -----------------------------------------------------------------
            foreach (Page page in extractedPages)
            {
                page.PageInfo.Width  = PageSize.A4.Width;   // 595 points
                page.PageInfo.Height = PageSize.A4.Height;  // 842 points
                // Optional: set orientation if needed
                page.PageInfo.IsLandscape = false;
            }

            // -----------------------------------------------------------------
            // 3. Remove the original pages from the document.
            //    Build an array of page numbers to delete.
            // -----------------------------------------------------------------
            int count = rangeEnd - rangeStart + 1;
            int[] pagesToDelete = new int[count];
            for (int i = 0; i < count; i++)
                pagesToDelete[i] = rangeStart + i;

            doc.Pages.Delete(pagesToDelete);   // removes the pages from the collection

            // -----------------------------------------------------------------
            // 4. Insert the resized pages at the new location.
            //    Insert expects the position where the first new page will appear.
            //    If insertAfter is 0 we insert at the beginning (position 1).
            // -----------------------------------------------------------------
            int insertPosition = insertAfter == 0 ? 1 : insertAfter + 1;
            doc.Pages.Insert(insertPosition, extractedPages.ToArray());

            // -----------------------------------------------------------------
            // 5. Save the modified document.
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages {rangeStart}-{rangeEnd} resized to A4 and re‑inserted. Output saved to '{outputPath}'.");
    }
}