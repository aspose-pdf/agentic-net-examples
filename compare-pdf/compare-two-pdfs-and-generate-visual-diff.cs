using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the source PDF files and the output comparison PDF
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(doc1Path))
        using (Document doc2 = new Document(doc2Path))
        {
            // Configure comparison options
            ComparisonOptions options = new ComparisonOptions();
            // The ComparisonOptions class does not expose an IgnoreCase property.
            // If case‑insensitive comparison is required, it must be handled manually
            // after obtaining the diff list. Here we simply use the default options.

            // Perform a flat (whole‑document) comparison and save the visual diff PDF
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPath);

            // Generate and display simple statistics about the differences
            TextItemComparisonStatistics stats = TextPdfComparer.CreateComparisonStatistics(diffs);
            Console.WriteLine($"Inserted characters : {stats.InsertedCharactersCount}");
            Console.WriteLine($"Deleted characters  : {stats.DeletedCharactersCount}");
            Console.WriteLine($"Insert operations   : {stats.InsertOperationsCount}");
            Console.WriteLine($"Delete operations   : {stats.DeleteOperationsCount}");
        }
    }
}