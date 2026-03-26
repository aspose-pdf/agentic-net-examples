using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPath = "doc1.pdf";
        const string secondPath = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"File not found: {firstPath}");
            return;
        }
        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"File not found: {secondPath}");
            return;
        }

        using (Document doc1 = new Document(firstPath))
        using (Document doc2 = new Document(secondPath))
        {
            ComparisonOptions options = new ComparisonOptions();
            // Options can be customized here if needed, e.g., options.DetectUnicodeDifferences = true;

            // Perform textual comparison and obtain a list of differences.
            List<DiffOperation> differences = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);
            Console.WriteLine($"Total differences detected: {differences.Count}");
            foreach (DiffOperation diff in differences)
            {
                Console.WriteLine(diff.ToString());
            }

            // Save a visual side‑by‑side comparison PDF.
            TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPath);
            Console.WriteLine($"Visual comparison saved to '{resultPath}'.");
        }
    }
}