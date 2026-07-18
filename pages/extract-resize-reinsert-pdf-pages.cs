using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Define the page range to extract (inclusive) and the position where they will be re‑inserted
        const int startPage = 3;   // first page of the range (1‑based)
        const int endPage   = 5;   // last page of the range (1‑based)
        const int insertAt  = 2;   // position in the remaining document where the extracted pages will be inserted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the requested range is valid
        if (startPage < 1 || endPage < startPage)
        {
            Console.Error.WriteLine("Invalid page range.");
            return;
        }

        // Work with the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains enough pages
            if (endPage > doc.Pages.Count)
            {
                Console.Error.WriteLine("Requested page range exceeds document page count.");
                return;
            }

            // -----------------------------------------------------------------
            // 1. Extract the required pages and keep them in a list
            // -----------------------------------------------------------------
            List<Page> extractedPages = new List<Page>();
            for (int i = startPage; i <= endPage; i++)
            {
                extractedPages.Add(doc.Pages[i]); // store reference to the page object
            }

            // -----------------------------------------------------------------
            // 2. Remove the extracted pages from the original document
            //    Deleting from the end towards the start preserves indices.
            // -----------------------------------------------------------------
            for (int i = endPage; i >= startPage; i--)
            {
                doc.Pages.Delete(i);
            }

            // -----------------------------------------------------------------
            // 3. Change the size of each extracted page to A4 (595 x 842 points)
            // -----------------------------------------------------------------
            const double a4Width  = 595; // points
            const double a4Height = 842; // points

            foreach (Page pg in extractedPages)
            {
                pg.PageInfo.Width  = a4Width;
                pg.PageInfo.Height = a4Height;
            }

            // -----------------------------------------------------------------
            // 4. Re‑insert the pages at the desired position
            //    Insert inserts before the specified index, so we use the
            //    provided insertAt value directly.
            // -----------------------------------------------------------------
            // Convert the list to an array because Insert expects Page[]
            Page[] pagesArray = extractedPages.ToArray();

            // Validate the insertion index (it can be Count+1 to append at the end)
            int maxInsertPos = doc.Pages.Count + 1;
            int safeInsertPos = Math.Max(1, Math.Min(insertAt, maxInsertPos));

            doc.Pages.Insert(safeInsertPos, pagesArray);

            // -----------------------------------------------------------------
            // 5. Save the modified document
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages {startPage}-{endPage} resized to A4 and re‑inserted at position {insertAt}.");
        Console.WriteLine($"Result saved to '{outputPath}'.");
    }
}