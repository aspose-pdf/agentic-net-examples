using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Pages to compare – 1‑based indexing as required by Aspose.Pdf
        int[] pagesToCompare = new int[] { 1, 2, 5 };

        using (Aspose.Pdf.Document doc1 = new Aspose.Pdf.Document(doc1Path))
        using (Aspose.Pdf.Document doc2 = new Aspose.Pdf.Document(doc2Path))
        {
            // Collect differences for each page pair
            List<List<DiffOperation>> allDiffs = new List<List<DiffOperation>>();
            ComparisonOptions options = new ComparisonOptions(); // default options; customize if needed

            foreach (int pageNum in pagesToCompare)
            {
                if (pageNum > doc1.Pages.Count || pageNum > doc2.Pages.Count)
                {
                    Console.WriteLine($"Page {pageNum} is out of range in one of the documents – skipping.");
                    continue;
                }

                Aspose.Pdf.Page page1 = doc1.Pages[pageNum];
                Aspose.Pdf.Page page2 = doc2.Pages[pageNum];

                // Perform text‑based comparison for the specific page pair
                List<DiffOperation> diffs = TextPdfComparer.ComparePages(page1, page2, options);
                allDiffs.Add(diffs);

                Console.WriteLine($"Page {pageNum}: {diffs.Count} differences detected.");
            }

            // Optional: generate a side‑by‑side visual comparison PDF for the selected pages
            using (Aspose.Pdf.Document tempDoc1 = new Aspose.Pdf.Document())
            using (Aspose.Pdf.Document tempDoc2 = new Aspose.Pdf.Document())
            {
                foreach (int pageNum in pagesToCompare)
                {
                    if (pageNum <= doc1.Pages.Count)
                        tempDoc1.Pages.Add(doc1.Pages[pageNum]);
                    if (pageNum <= doc2.Pages.Count)
                        tempDoc2.Pages.Add(doc2.Pages[pageNum]);
                }

                SideBySideComparisonOptions sideOptions = new SideBySideComparisonOptions();
                Aspose.Pdf.Comparison.SideBySidePdfComparer.Compare(tempDoc1, tempDoc2, resultPath, sideOptions);
                Console.WriteLine($"Side‑by‑side comparison PDF saved to '{resultPath}'.");
            }
        }
    }
}