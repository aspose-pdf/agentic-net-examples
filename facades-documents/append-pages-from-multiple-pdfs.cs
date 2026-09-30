using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the final PDF and the source PDFs
        const string outputPath = "merged.pdf";
        string[] sourcePaths = { "source1.pdf", "source2.pdf", "source3.pdf" };
        // Corresponding page ranges for each source PDF (Aspose.Pdf page range syntax)
        string[] pageRanges = { "1-2", "3,5-6", "1-" };

        // -----------------------------------------------------------------
        // Step 1: Create an empty target PDF file (placeholder page).
        // -----------------------------------------------------------------
        using (var placeholderDoc = new Document())
        {
            // Add a single blank page – it will be removed later if not needed.
            placeholderDoc.Pages.Add();
            placeholderDoc.Save(outputPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Insert pages from each source PDF according to the ranges.
        // -----------------------------------------------------------------
        // Load the target document once – we will keep it in memory and save at the end.
        var targetDoc = new Document(outputPath);
        int insertPosition = targetDoc.Pages.Count + 1; // start after placeholder

        for (int i = 0; i < sourcePaths.Length; i++)
        {
            string srcPath = sourcePaths[i];
            if (!File.Exists(srcPath))
            {
                Console.WriteLine($"Warning: Source file '{srcPath}' not found – skipping.");
                continue; // skip missing file instead of throwing an exception
            }

            // Load the current source PDF.
            using (var srcDoc = new Document(srcPath))
            {
                // Resolve the page numbers that need to be copied.
                List<int> pagesToCopy = ParsePageRange(pageRanges[i], srcDoc.Pages.Count);

                // Insert each required page into the target document.
                foreach (int pageNumber in pagesToCopy)
                {
                    // Aspose.Pdf pages are 1‑based.
                    // The Insert method copies the page, so we can safely use the page from srcDoc.
                    targetDoc.Pages.Insert(insertPosition, srcDoc.Pages[pageNumber]);
                    insertPosition++; // next insertion goes after the newly added page
                }
            }
        }

        // -----------------------------------------------------------------
        // Step 3: Clean up the initial placeholder page (if it is still empty).
        // -----------------------------------------------------------------
        if (targetDoc.Pages.Count > 0 && targetDoc.Pages[1].Contents.Count == 0)
        {
            targetDoc.Pages.Delete(1);
        }

        // Save the final merged PDF.
        targetDoc.Save("final_output.pdf");
        Console.WriteLine("Pages inserted and merged PDF saved as 'final_output.pdf'.");
    }

    /// <summary>
    /// Parses a page‑range string (e.g., "1-2", "3,5-6", "1-") into a list of page numbers.
    /// The method respects Aspose.Pdf's 1‑based page indexing.
    /// </summary>
    private static List<int> ParsePageRange(string range, int totalPages)
    {
        var result = new List<int>();
        if (string.IsNullOrWhiteSpace(range))
            return result;

        // Split by commas to handle multiple segments.
        string[] parts = range.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string part in parts)
        {
            string trimmed = part.Trim();
            if (trimmed.Contains("-"))
            {
                // Handle a range like "3-6" or "1-" (open‑ended).
                string[] bounds = trimmed.Split('-');
                int start = int.Parse(bounds[0]);
                int end;
                if (string.IsNullOrEmpty(bounds[1]))
                {
                    // Open‑ended range – go to the last page.
                    end = totalPages;
                }
                else
                {
                    end = int.Parse(bounds[1]);
                }
                // Clamp values to the document's page count.
                start = Math.Max(1, start);
                end = Math.Min(totalPages, end);
                for (int p = start; p <= end; p++)
                    result.Add(p);
            }
            else
            {
                // Single page number.
                int page = int.Parse(trimmed);
                if (page >= 1 && page <= totalPages)
                    result.Add(page);
            }
        }
        return result;
    }
}
