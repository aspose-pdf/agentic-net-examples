using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdf = "doc1.pdf";
        const string secondPdf = "doc2.pdf";
        const string resultPdf = "compare_result.pdf"; // retained for compatibility – not used by TextPdfComparer

        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            using (Document doc1 = new Document(firstPdf))
            using (Document doc2 = new Document(secondPdf))
            {
                // Configure comparison options as needed
                ComparisonOptions options = new ComparisonOptions();
                // Example: highlight differences in yellow (optional)
                // options.HighlightColor = Aspose.Pdf.Color.Yellow;

                // Define the pages to compare (1‑based indexing)
                int[] pagesToCompare = new int[] { 1, 2, 4 };

                // Compare the selected pages one‑by‑one using TextPdfComparer
                foreach (int pageNumber in pagesToCompare)
                {
                    // Ensure the page exists in both documents
                    if (pageNumber > doc1.Pages.Count || pageNumber > doc2.Pages.Count)
                    {
                        Console.WriteLine($"Page {pageNumber} does not exist in one of the documents – skipping.");
                        continue;
                    }

                    Page page1 = doc1.Pages[pageNumber];
                    Page page2 = doc2.Pages[pageNumber];

                    // Perform the comparison for the current page pair
                    var diffs = TextPdfComparer.ComparePages(page1, page2, options);

                    // Simple reporting – you can extend this to generate a visual diff PDF if needed
                    Console.WriteLine($"Compared page {pageNumber}: {diffs.Count} difference(s) found.");
                }

                // If you need a visual diff PDF you would have to use the newer DocumentComparer API
                // which is not available in the referenced Aspose.Pdf version.
                Console.WriteLine("Page‑by‑page comparison completed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}
