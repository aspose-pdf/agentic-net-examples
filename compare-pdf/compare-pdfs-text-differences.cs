using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the two PDF files to compare
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load the documents using the core Document API (no Facades)
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create comparison options.
            // NOTE: The ComparisonOptions class does not expose an IgnoreCase property.
            // To achieve case‑insensitive comparison you would need to preprocess the
            // text (e.g., convert both documents to lower case) or use a regex‑based
            // approach. Here we proceed with the default options.
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat (whole‑document) comparison.
            // The result is a list of DiffOperation objects describing the changes.
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);

            // Optionally, you can save a visual PDF report of the differences.
            // The overload that accepts a result path creates a side‑by‑side PDF.
            TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPdfPath);

            // Output a simple summary to the console.
            Console.WriteLine($"Comparison completed. Total differences: {diffs.Count}");
        }
    }
}