using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string file1 = "input1.pdf";
        const string file2 = "input2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!System.IO.File.Exists(file1) || !System.IO.File.Exists(file2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        using (Document doc1 = new Document(file1))
        using (Document doc2 = new Document(file2))
        {
            // Default comparison options – can be customized if needed
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat (whole‑document) comparison and save the visual diff PDF
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPath);

            Console.WriteLine($"Comparison completed. Number of differences: {diffs.Count}");
            Console.WriteLine($"Result PDF saved to '{resultPath}'.");
        }
    }
}