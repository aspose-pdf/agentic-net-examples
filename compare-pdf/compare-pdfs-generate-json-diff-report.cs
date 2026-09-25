using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string oldPdfPath = "old.pdf";
        const string newPdfPath = "new.pdf";
        const string jsonReportPath = "diff_report.json";

        if (!File.Exists(oldPdfPath) || !File.Exists(newPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDFs using the core Document class
        Document doc1 = new Document(oldPdfPath);
        Document doc2 = new Document(newPdfPath);

        // Use the text‑based comparer that returns DiffOperation objects
        ComparisonOptions compareOptions = new ComparisonOptions(); // defaults are fine

        // Let the compiler infer the exact return type to avoid IList conversion issues
        var pageDifferences = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, compareOptions);

        // Convert DiffOperation objects into a serializable structure
        var reportItems = new List<DiffReportItem>();
        for (int i = 0; i < pageDifferences.Count; i++)
        {
            IList<DiffOperation> diffs = pageDifferences[i];
            if (diffs == null) continue; // no differences on this page
            foreach (DiffOperation diff in diffs)
            {
                reportItems.Add(new DiffReportItem
                {
                    PageNumber = i + 1,               // pages are 1‑based for the report
                    Operation   = diff.Operation.ToString(), // enum → string
                    Details     = diff.Text         // the text fragment involved in the diff
                });
            }
        }

        // Serialize the report to JSON with indentation for readability
        string json = JsonSerializer.Serialize(reportItems, new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON report to disk
        File.WriteAllText(jsonReportPath, json);
        Console.WriteLine($"Diff report saved to '{jsonReportPath}'.");
    }

    // DTO used for JSON serialization – properties are nullable to satisfy non‑nullable warnings
    private class DiffReportItem
    {
        public int    PageNumber { get; set; }
        public string? Operation   { get; set; }
        public string? Details     { get; set; }
    }
}