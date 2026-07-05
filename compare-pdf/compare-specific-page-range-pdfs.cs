using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Define the page range to compare (inclusive)
        const int startPage = 2;
        const int endPage   = 4;

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Ensure the requested range does not exceed the actual page count of either document
                int maxPage = Math.Min(endPage, Math.Min(doc1.Pages.Count, doc2.Pages.Count));
                if (startPage > maxPage)
                {
                    Console.Error.WriteLine("Start page is beyond the number of pages in the documents.");
                    return;
                }

                // Comparison options – keep defaults (no start/end page properties)
                ComparisonOptions options = new ComparisonOptions();

                // Store differences per page
                List<List<DiffOperation>> diffPerPage = new List<List<DiffOperation>>();

                for (int pageNumber = startPage; pageNumber <= maxPage; pageNumber++)
                {
                    // Compare the corresponding pages from both documents
                    List<DiffOperation> diffs = TextPdfComparer.ComparePages(doc1.Pages[pageNumber], doc2.Pages[pageNumber], options);
                    diffPerPage.Add(diffs);
                }

                // Simple output: number of differences per compared page
                for (int i = 0; i < diffPerPage.Count; i++)
                {
                    int pageNumber = startPage + i;
                    int diffCount  = diffPerPage[i]?.Count ?? 0;
                    Console.WriteLine($"Page {pageNumber}: {diffCount} differences found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}
