using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string resultPdf = "diff_result.pdf";

        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        using (Document doc1 = new Document(firstPdf))
        using (Document doc2 = new Document(secondPdf))
        {
            ComparisonOptions options = new ComparisonOptions();
            // If needed, enable font‑difference detection (property name may vary by version)
            // options.DetectFontDifferences = true;

            List<DiffOperation> diffOperations = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPdf);

            Console.WriteLine($"Total diff operations: {diffOperations.Count}");
            foreach (DiffOperation diff in diffOperations)
            {
                Console.WriteLine(diff.ToString());
            }
        }
    }
}