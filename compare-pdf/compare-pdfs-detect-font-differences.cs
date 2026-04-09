using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the PDFs to compare
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";
        // Path for the visual diff result (optional)
        const string diffResultPath = "diffResult.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create default comparison options.
            // No special settings are required to detect font changes;
            // the comparer will report font differences as separate DiffOperation items.
            ComparisonOptions options = new ComparisonOptions();

            // Perform a page‑by‑page textual comparison.
            // The method returns a list of diff operations for each page.
            var diffsByPage = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Optionally generate a PDF that visualizes the differences.
            // This overload saves the visual diff to the specified file.
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, diffResultPath);

            // Analyze the diff results.
            int totalDiffOps = 0;
            int fontDiffOps = 0;

            foreach (var pageDiffs in diffsByPage)
            {
                totalDiffOps += pageDiffs.Count;
                foreach (var diff in pageDiffs)
                {
                    // Output the diff description (DiffOperation overrides ToString()).
                    Console.WriteLine(diff);

                    // Simple heuristic: count operations whose description mentions "Font".
                    // This demonstrates that font changes are reported as distinct diff operations.
                    if (diff.ToString().IndexOf("Font", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        fontDiffOps++;
                    }
                }
            }

            Console.WriteLine($"Total diff operations: {totalDiffOps}");
            Console.WriteLine($"Font‑related diff operations: {fontDiffOps}");
        }
    }
}