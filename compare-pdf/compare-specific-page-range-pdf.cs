using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";

        // Verify that both source files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Desired page range (1‑based indexing)
            int startPage = 2;
            int endPage   = 4;

            // Clamp the range to the actual number of pages in both documents
            int maxPage = Math.Min(Math.Min(doc1.Pages.Count, doc2.Pages.Count), endPage);
            startPage = Math.Max(1, startPage);
            if (startPage > maxPage)
            {
                Console.Error.WriteLine("Start page is beyond the number of pages in the documents.");
                return;
            }

            // ComparisonOptions does not expose StartPage/EndPage in the current API.
            // Use the default options and later filter the result list to the required range.
            ComparisonOptions options = new ComparisonOptions();

            // Perform the comparison for all pages; the method returns a list of diff operations per page.
            // The concrete return type is List<List<DiffOperation>> which is compatible with IList<List<DiffOperation>>.
            List<List<DiffOperation>> diffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Report only the pages that fall inside the requested range.
            for (int i = startPage - 1; i < maxPage; i++)
            {
                int pageNumber = i + 1; // convert back to 1‑based page number
                int diffCount = diffs[i]?.Count ?? 0;
                Console.WriteLine($"Page {pageNumber}: {diffCount} differences found.");
            }
        }
    }
}
