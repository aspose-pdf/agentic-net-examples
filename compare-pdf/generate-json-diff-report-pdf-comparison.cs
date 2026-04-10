using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string jsonReportPath = "diffReport.json";

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create default comparison options
            ComparisonOptions options = new ComparisonOptions();

            // Perform a page‑by‑page text comparison
            // Returns a list where each element corresponds to a page and contains its DiffOperation list
            List<List<DiffOperation>> pageDiffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Optional: iterate over the DiffOperation objects and write a simple console report
            for (int pageIndex = 0; pageIndex < pageDiffs.Count; pageIndex++)
            {
                Console.WriteLine($"Differences on page {pageIndex + 1}:");
                foreach (DiffOperation diff in pageDiffs[pageIndex])
                {
                    // DiffOperation.Operation indicates Insert, Delete, Equal, etc.
                    Console.WriteLine($"  {diff.Operation}: {diff.Text}");
                }
            }

            // Generate a JSON report of the differences and save it to a file
            JsonDiffOutputGenerator jsonGenerator = new JsonDiffOutputGenerator();
            jsonGenerator.GenerateOutput(pageDiffs, jsonReportPath);
        }

        Console.WriteLine($"JSON diff report saved to '{jsonReportPath}'.");
    }
}