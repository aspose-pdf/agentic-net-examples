using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the final merged PDF and the source PDFs
        const string targetPath = "merged.pdf";
        string[] sourcePaths = { "source1.pdf", "source2.pdf", "source3.pdf" };
        // Corresponding page ranges for each source PDF (e.g., "1-2,5")
        string[] pageRanges = { "1-2,5", "3-4", "1-3" };

        // -----------------------------------------------------------------
        // 1. Create an empty PDF document that will receive the inserted pages.
        // -----------------------------------------------------------------
        Document targetDoc = new Document();

        // -----------------------------------------------------------------
        // 2. Iterate over each source PDF and its page range, copying the pages
        //    into the target document.
        // -----------------------------------------------------------------
        for (int i = 0; i < sourcePaths.Length; i++)
        {
            string srcPath = sourcePaths[i];
            string range   = pageRanges[i];

            if (!File.Exists(srcPath))
            {
                Console.Error.WriteLine($"Source file not found: {srcPath}");
                continue;
            }

            // Load the source PDF.
            Document srcDoc = new Document(srcPath);

            // Parse the page range string into a list of 1‑based page numbers.
            List<int> pagesToCopy = ParsePageRange(range, srcDoc.Pages.Count);

            // Copy each requested page to the end of the target document.
            foreach (int pageNumber in pagesToCopy)
            {
                // Aspose.Pdf allows adding a page from another document directly.
                // The page is cloned internally, so the source document can be disposed.
                targetDoc.Pages.Add(srcDoc.Pages[pageNumber]);
            }
        }

        // Save the merged PDF.
        targetDoc.Save(targetPath);

        // -----------------------------------------------------------------
        // 3. Load the final document to confirm the operation succeeded.
        // -----------------------------------------------------------------
        using (Document result = new Document(targetPath))
        {
            Console.WriteLine($"Merged PDF created: {targetPath}");
            Console.WriteLine($"Total pages: {result.Pages.Count}");
        }
    }

    /// <summary>
    /// Parses a page‑range string such as "1-2,5" into a list of individual page numbers.
    /// The method validates that each number is within the supplied maxPageCount.
    /// </summary>
    private static List<int> ParsePageRange(string range, int maxPageCount)
    {
        var result = new List<int>();
        if (string.IsNullOrWhiteSpace(range))
            return result;

        string[] parts = range.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string part in parts)
        {
            if (part.Contains("-"))
            {
                string[] bounds = part.Split('-');
                if (bounds.Length != 2) continue;
                if (int.TryParse(bounds[0], out int start) && int.TryParse(bounds[1], out int end))
                {
                    // Ensure start <= end and both are within the document.
                    start = Math.Max(start, 1);
                    end   = Math.Min(end, maxPageCount);
                    for (int p = start; p <= end; p++)
                        result.Add(p);
                }
            }
            else
            {
                if (int.TryParse(part, out int page))
                {
                    if (page >= 1 && page <= maxPageCount)
                        result.Add(page);
                }
            }
        }
        return result;
    }
}
