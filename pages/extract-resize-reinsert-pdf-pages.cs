using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Define the range of pages to extract (1‑based inclusive)
        const int rangeStart = 2;   // first page of the range
        const int rangeEnd   = 4;   // last page of the range

        // Define the position where the extracted pages will be re‑inserted
        // (1‑based index where the first new page will appear)
        const int insertPosition = 6;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Collect the pages in the requested range
            // -----------------------------------------------------------------
            List<Page> extractedPages = new List<Page>();
            for (int i = rangeStart; i <= rangeEnd; i++)
            {
                extractedPages.Add(doc.Pages[i]); // Pages are 1‑based
            }

            // -----------------------------------------------------------------
            // 2. Change each extracted page size to A4
            // -----------------------------------------------------------------
            foreach (Page pg in extractedPages)
            {
                // PageSize.A4 provides Width and Height in points (1/72 inch)
                pg.PageInfo.Width  = Aspose.Pdf.PageSize.A4.Width;
                pg.PageInfo.Height = Aspose.Pdf.PageSize.A4.Height;
                // Optional: ensure portrait orientation
                pg.PageInfo.IsLandscape = false;
            }

            // -----------------------------------------------------------------
            // 3. Remove the original pages from the document
            // -----------------------------------------------------------------
            int pagesCount = rangeEnd - rangeStart + 1;
            int[] pagesToDelete = Enumerable.Range(rangeStart, pagesCount).ToArray();
            doc.Pages.Delete(pagesToDelete);

            // -----------------------------------------------------------------
            // 4. Adjust the insertion index if it was after the deleted range
            // -----------------------------------------------------------------
            int adjustedInsertPos = insertPosition;
            if (insertPosition > rangeEnd)
            {
                adjustedInsertPos -= pagesCount;
            }

            // -----------------------------------------------------------------
            // 5. Re‑insert the modified pages at the new position
            // -----------------------------------------------------------------
            doc.Pages.Insert(adjustedInsertPos, extractedPages.ToArray());

            // -----------------------------------------------------------------
            // 6. Save the resulting PDF
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
