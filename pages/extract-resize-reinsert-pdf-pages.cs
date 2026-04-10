using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Text;                // For any text-related types (not used here)

class Program
{
    static void Main()
    {
        // Input / output paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Define the range of pages to extract (1‑based inclusive)
        const int rangeStart = 2;   // example: start at page 2
        const int rangeEnd   = 4;   // example: end at page 4

        // Define the position where the extracted pages will be re‑inserted
        // (1‑based). After removal the collection size changes, so adjust accordingly.
        const int insertPosition = 1; // insert at the beginning

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate range
            if (rangeStart < 1 || rangeEnd > doc.Pages.Count || rangeStart > rangeEnd)
            {
                Console.Error.WriteLine("Invalid page range.");
                return;
            }

            // -----------------------------------------------------------------
            // 1. Extract the pages into a temporary list
            // -----------------------------------------------------------------
            List<Page> extractedPages = new List<Page>();
            for (int i = rangeStart; i <= rangeEnd; i++)
            {
                // Store a reference to the page object
                extractedPages.Add(doc.Pages[i]);
            }

            // -----------------------------------------------------------------
            // 2. Remove the extracted pages from the original document
            //    (delete in descending order to keep indices stable)
            // -----------------------------------------------------------------
            for (int i = rangeEnd; i >= rangeStart; i--)
            {
                doc.Pages.Delete(i);
            }

            // -----------------------------------------------------------------
            // 3. Change each extracted page size to A4 (595 x 842 points)
            // -----------------------------------------------------------------
            const double a4Width  = 595; // points
            const double a4Height = 842; // points
            foreach (Page pg in extractedPages)
            {
                // Set the page rectangle to A4 dimensions
                pg.Rect = new Aspose.Pdf.Rectangle(0, 0, a4Width, a4Height);
            }

            // -----------------------------------------------------------------
            // 4. Re‑insert the pages at the desired position
            // -----------------------------------------------------------------
            // Insert overload that accepts an array of Page objects
            doc.Pages.Insert(insertPosition, extractedPages.ToArray());

            // -----------------------------------------------------------------
            // 5. Save the modified document
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages {rangeStart}-{rangeEnd} resized to A4 and re‑inserted at position {insertPosition}.");
        Console.WriteLine($"Result saved to '{outputPath}'.");
    }
}