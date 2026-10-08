using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the page range to extract and the position where it will be reinserted.
        // Example: extract pages 2‑4 and insert them after page 6.
        int rangeStart = 2;
        int rangeEnd   = 4;
        int insertAfter = 6; // pages will be inserted starting at this index + 1

        // Load the source PDF inside a using block for deterministic disposal.
        using (Document src = new Document(inputPath))
        {
            // Validate the requested range.
            if (rangeStart < 1 || rangeEnd > src.Pages.Count || rangeStart > rangeEnd)
            {
                Console.Error.WriteLine("Invalid page range.");
                return;
            }

            // -----------------------------------------------------------------
            // 1. Copy the required pages to a temporary document.
            // -----------------------------------------------------------------
            Document extracted = new Document();
            for (int i = rangeStart; i <= rangeEnd; i++)
            {
                // Adding a page to another document creates a deep copy.
                extracted.Pages.Add(src.Pages[i]);
            }

            // -----------------------------------------------------------------
            // 2. Resize each extracted page to A4 (595 × 842 points).
            // -----------------------------------------------------------------
            foreach (Page page in extracted.Pages)
            {
                page.PageInfo.Width  = PageSize.A4.Width;
                page.PageInfo.Height = PageSize.A4.Height;
            }

            // -----------------------------------------------------------------
            // 3. Remove the original pages from the source document.
            // -----------------------------------------------------------------
            // Aspose.Pdf.Pages.Delete only accepts a single page number, so delete
            // the range backwards to keep the remaining indices valid.
            for (int i = rangeEnd; i >= rangeStart; i--)
            {
                src.Pages.Delete(i);
            }

            // -----------------------------------------------------------------
            // 4. Compute the insertion index (Aspose.Pdf uses 1‑based indexing).
            // -----------------------------------------------------------------
            int insertIndex = insertAfter + 1;
            if (insertIndex > src.Pages.Count + 1)
                insertIndex = src.Pages.Count + 1;

            // -----------------------------------------------------------------
            // 5. Re‑insert the extracted pages at the new position, preserving order.
            // -----------------------------------------------------------------
            foreach (Page page in extracted.Pages)
            {
                src.Pages.Insert(insertIndex, page);
                insertIndex++; // advance so subsequent pages follow the previous one
            }

            // Save the modified document.
            src.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
