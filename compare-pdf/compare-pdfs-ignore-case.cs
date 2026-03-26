using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "doc1.pdf";
        const string secondPdfPath = "doc2.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        using (Document firstDoc = new Document(firstPdfPath))
        using (Document secondDoc = new Document(secondPdfPath))
        {
            ComparisonOptions options = new ComparisonOptions();
            // NOTE: Aspose.Pdf.Comparison.ComparisonOptions does NOT expose an IgnoreCase property.
            // To perform a case‑insensitive comparison you must preprocess the PDFs (e.g., convert all
            // extracted text to the same case) before invoking the comparer, or implement a custom
            // diff logic after the comparison.

            List<DiffOperation> differences = TextPdfComparer.CompareFlatDocuments(firstDoc, secondDoc, options, resultPdfPath);

            Console.WriteLine($"Comparison finished. Number of differences: {differences.Count}");
        }
    }
}