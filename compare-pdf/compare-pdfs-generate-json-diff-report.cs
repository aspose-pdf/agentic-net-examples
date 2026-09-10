using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// Simple representation of a diff operation – mimics the Aspose.Pdf.Comparison DiffOperation
public class DiffOperation
{
    public string OperationType { get; set; }
    public int PageNumber { get; set; }
    public string Details { get; set; }
}

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
            Console.Error.WriteLine("One or both PDF files were not found.");
            return;
        }

        // Load the two PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Perform a very basic text‑based comparison page by page.
            // This replaces the missing Aspose.Pdf.Comparison API while still
            // producing a collection of DiffOperation objects that can be
            // serialized to JSON.
            List<DiffOperation> diffOperations = CompareDocuments(doc1, doc2);

            // Serialize the diff operations to JSON (indented for readability)
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonReport = JsonSerializer.Serialize(diffOperations, jsonOptions);

            // Write the JSON report to a file
            File.WriteAllText(jsonReportPath, jsonReport);
            Console.WriteLine($"Diff report saved to '{jsonReportPath}'.");
        }
    }

    /// <summary>
    /// Compares two Aspose.Pdf.Document objects page by page using their extracted text.
    /// Returns a list of DiffOperation objects describing the differences.
    /// </summary>
    private static List<DiffOperation> CompareDocuments(Document doc1, Document doc2)
    {
        var diffs = new List<DiffOperation>();
        int maxPages = Math.Max(doc1.Pages.Count, doc2.Pages.Count);

        for (int i = 1; i <= maxPages; i++)
        {
            string text1 = i <= doc1.Pages.Count ? ExtractPageText(doc1, i) : string.Empty;
            string text2 = i <= doc2.Pages.Count ? ExtractPageText(doc2, i) : string.Empty;

            if (text1 != text2)
            {
                // Simple heuristic: if one side is empty, treat as Added/Removed, otherwise Modified
                string operationType;
                if (string.IsNullOrEmpty(text1) && !string.IsNullOrEmpty(text2))
                    operationType = "Added";
                else if (!string.IsNullOrEmpty(text1) && string.IsNullOrEmpty(text2))
                    operationType = "Removed";
                else
                    operationType = "Modified";

                diffs.Add(new DiffOperation
                {
                    OperationType = operationType,
                    PageNumber = i,
                    Details = operationType == "Added" ? text2 : text1
                });
            }
        }

        return diffs;
    }

    /// <summary>
    /// Extracts all visible text from a given page using Aspose.Pdf.Text.TextAbsorber.
    /// </summary>
    private static string ExtractPageText(Document doc, int pageNumber)
    {
        var absorber = new TextAbsorber();
        absorber.Visit(doc.Pages[pageNumber]);
        return absorber.Text ?? string.Empty;
    }
}
