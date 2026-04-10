using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Output PDF that will contain the side‑by‑side comparison
        const string resultPdfPath = "comparison_result.pdf";

        // Pages to compare (1‑based indexing)
        int[] pagesToCompare = new int[] { 1, 3, 5 };

        // Verify that the source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the original documents
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create temporary documents that will contain only the selected pages
            using (Document subset1 = new Document())
            using (Document subset2 = new Document())
            {
                // Copy the requested pages from the first document
                foreach (int pageNum in pagesToCompare)
                {
                    if (pageNum >= 1 && pageNum <= doc1.Pages.Count)
                        subset1.Pages.Add(doc1.Pages[pageNum]);
                }

                // Copy the requested pages from the second document
                foreach (int pageNum in pagesToCompare)
                {
                    if (pageNum >= 1 && pageNum <= doc2.Pages.Count)
                        subset2.Pages.Add(doc2.Pages[pageNum]);
                }

                // Configure side‑by‑side comparison options (defaults are fine for a basic example)
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Perform the comparison and save the result
                SideBySidePdfComparer.Compare(subset1, subset2, resultPdfPath, options);
            }
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}