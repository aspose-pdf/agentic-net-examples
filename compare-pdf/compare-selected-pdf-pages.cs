using System;
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

        // Output file that will contain the comparison result
        const string resultPdfPath = "comparison_result.pdf";

        // Page numbers to compare (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToCompare = new int[] { 1, 3, 5 };

        // Verify that the source files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the original documents inside using blocks for deterministic disposal
        using (Document sourceDoc1 = new Document(pdfPath1))
        using (Document sourceDoc2 = new Document(pdfPath2))
        {
            // Create temporary documents that will contain only the selected pages
            using (Document subsetDoc1 = new Document())
            using (Document subsetDoc2 = new Document())
            {
                // Copy the requested pages from the first source document
                foreach (int pageNum in pagesToCompare)
                {
                    // Guard against out‑of‑range page numbers
                    if (pageNum >= 1 && pageNum <= sourceDoc1.Pages.Count)
                    {
                        subsetDoc1.Pages.Add(sourceDoc1.Pages[pageNum]);
                    }
                }

                // Copy the requested pages from the second source document
                foreach (int pageNum in pagesToCompare)
                {
                    if (pageNum >= 1 && pageNum <= sourceDoc2.Pages.Count)
                    {
                        subsetDoc2.Pages.Add(sourceDoc2.Pages[pageNum]);
                    }
                }

                // Set up comparison options (default options are sufficient for a basic comparison)
                ComparisonOptions options = new ComparisonOptions();

                // Perform the page‑by‑page text comparison and save the visual result
                // The overload with a result path returns the list of differences per page
                var differences = TextPdfComparer.CompareDocumentsPageByPage(
                    subsetDoc1,
                    subsetDoc2,
                    options,
                    resultPdfPath);

                // Output a simple summary to the console
                Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
                Console.WriteLine($"Number of pages compared: {differences.Count}");
                for (int i = 0; i < differences.Count; i++)
                {
                    Console.WriteLine($"Page {pagesToCompare[i]}: {differences[i].Count} change(s) detected.");
                }
            }
        }
    }
}