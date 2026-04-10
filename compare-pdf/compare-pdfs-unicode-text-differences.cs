using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "doc_en.pdf";
        const string pdfPath2 = "doc_ru.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!System.IO.File.Exists(pdfPath1) || !System.IO.File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents to be compared
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // ComparisonOptions have text comparison enabled by default – no need to set EnableTextComparison
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat comparison: all pages are concatenated into a single text block.
            // The result PDF with visual differences is saved to resultPath.
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPath);

            // Report the detected differences
            Console.WriteLine($"Total differences detected: {diffs.Count}");
            foreach (DiffOperation diff in diffs)
            {
                // DiffOperation provides the Operation enum and the differing text.
                // Additional information such as page index can be obtained via ToString() if needed.
                Console.WriteLine($"{diff.Operation} – \"{diff.Text}\"");
            }

            Console.WriteLine($"Comparison PDF saved to: {resultPath}");
        }
    }
}
