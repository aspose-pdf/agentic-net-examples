using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Define the range of pages to extract (1‑based inclusive)
        const int rangeStart = 2;
        const int rangeEnd   = 4;

        // Define the position where the extracted pages will be re‑inserted
        // (1‑based). Adjust if the position is after the original range.
        const int insertPosition = 6;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Copy the selected pages into a temporary document
            // -----------------------------------------------------------------
            Document tempDoc = new Document();
            for (int i = rangeStart; i <= rangeEnd; i++)
            {
                // Add a reference to the page; this creates a new page in tempDoc
                tempDoc.Pages.Add(srcDoc.Pages[i]);
            }

            // -----------------------------------------------------------------
            // 2. Change the size of the copied pages to A4
            // -----------------------------------------------------------------
            foreach (Page p in tempDoc.Pages)
            {
                // SetPageSize expects width and height in points (1 point = 1/72 inch)
                p.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
            }

            // -----------------------------------------------------------------
            // 3. Remove the original pages from the source document
            //    Deleting from the end prevents index shifting.
            // -----------------------------------------------------------------
            for (int i = rangeEnd; i >= rangeStart; i--)
            {
                srcDoc.Pages.Delete(i);
            }

            // -----------------------------------------------------------------
            // 4. Insert the resized pages at the desired position
            // -----------------------------------------------------------------
            // If the insertion point was after the removed range, adjust it.
            int adjustedInsertPos = insertPosition;
            if (insertPosition > rangeEnd)
                adjustedInsertPos -= (rangeEnd - rangeStart + 1);

            // Insert the array of pages from the temporary document
            srcDoc.Pages.Insert(adjustedInsertPos, tempDoc.Pages.ToArray());

            // -----------------------------------------------------------------
            // 5. Save the modified PDF
            // -----------------------------------------------------------------
            srcDoc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}