using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputFolder = "diff_pages";

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // Ensure the folder for diff pages exists
        Directory.CreateDirectory(outputFolder);

        // Define the pages to compare (1‑based indexing as per Aspose.Pdf)
        int[] pagesToCompare = new int[] { 1, 2, 3 }; // compare pages 1, 2 and 3

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            var options = new SideBySideComparisonOptions
            {
                // optional visual settings
                AdditionalChangeMarks = true,
                ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            foreach (int pageNumber in pagesToCompare)
            {
                // Validate that the page number exists in both documents
                if (pageNumber < 1 || pageNumber > Math.Min(doc1.Pages.Count, doc2.Pages.Count))
                {
                    Console.WriteLine($"Page {pageNumber} is out of range – skipping.");
                    continue;
                }

                string diffPath = Path.Combine(outputFolder, $"diff_page{pageNumber}.pdf");

                // Perform side‑by‑side visual comparison for the specific page
                SideBySidePdfComparer.Compare(doc1.Pages[pageNumber], doc2.Pages[pageNumber], diffPath, options);
                Console.WriteLine($"Compared page {pageNumber}, result saved to '{diffPath}'.");
            }
        }
    }
}
