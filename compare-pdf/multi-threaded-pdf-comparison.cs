using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfComparisonResult
{
    public string FileA { get; set; }
    public string FileB { get; set; }
    public bool AreEqual { get; set; }
    public string Differences { get; set; }

    // Initialise non‑nullable properties to avoid CS8618 warnings.
    public PdfComparisonResult()
    {
        FileA = string.Empty;
        FileB = string.Empty;
        Differences = string.Empty;
        AreEqual = true;
    }
}

class Program
{
    // Extracts the full text of a single page.
    static string ExtractPageText(Document doc, int pageNumber)
    {
        // Pages are 1‑based (see GLOBAL RULE: page-indexing-one-based)
        // TextAbsorber does not implement IDisposable in the current SDK version, so avoid using.
        TextAbsorber absorber = new TextAbsorber();
        doc.Pages[pageNumber].Accept(absorber);
        string text = absorber.Text ?? string.Empty;
        // No need to dispose explicitly.
        return text;
    }

    // Compares two PDFs and returns a result object.
    static PdfComparisonResult ComparePdfs(string pathA, string pathB)
    {
        var result = new PdfComparisonResult
        {
            FileA = pathA,
            FileB = pathB,
            AreEqual = true,
            Differences = string.Empty
        };

        // Ensure both files exist before proceeding.
        if (!File.Exists(pathA) || !File.Exists(pathB))
        {
            result.AreEqual = false;
            result.Differences = "One or both files do not exist.";
            return result;
        }

        // Load both documents inside using blocks (see GLOBAL RULE: document-disposal-with-using)
        using (Document docA = new Document(pathA))
        using (Document docB = new Document(pathB))
        {
            // Compare page counts.
            if (docA.Pages.Count != docB.Pages.Count)
            {
                result.AreEqual = false;
                result.Differences = $"Page count mismatch: {docA.Pages.Count} vs {docB.Pages.Count}.";
                return result;
            }

            // Compare text content page by page.
            for (int i = 1; i <= docA.Pages.Count; i++) // 1‑based loop
            {
                string textA = ExtractPageText(docA, i);
                string textB = ExtractPageText(docB, i);

                if (!string.Equals(textA, textB, StringComparison.Ordinal))
                {
                    result.AreEqual = false;
                    result.Differences = $"Text differs on page {i}.";
                    // Early exit on first difference; remove break to collect all differences.
                    break;
                }
            }
        }

        return result;
    }

    static async Task Main(string[] args)
    {
        // Define pairs of PDFs to compare.
        var pdfPairs = new List<(string FileA, string FileB)>
        {
            ("doc1_v1.pdf", "doc1_v2.pdf"),
            ("report_Jan.pdf", "report_Feb.pdf"),
            ("manual_en.pdf", "manual_fr.pdf")
        };

        // Create a task for each comparison.
        var comparisonTasks = new List<Task<PdfComparisonResult>>();
        foreach (var (fileA, fileB) in pdfPairs)
        {
            // Capture variables correctly for the lambda.
            string a = fileA;
            string b = fileB;
            comparisonTasks.Add(Task.Run(() => ComparePdfs(a, b)));
        }

        // Wait for all tasks to finish.
        PdfComparisonResult[] results = await Task.WhenAll(comparisonTasks);

        // Output the aggregated results.
        foreach (var res in results)
        {
            Console.WriteLine($"Comparison: {Path.GetFileName(res.FileA)} ↔ {Path.GetFileName(res.FileB)}");
            Console.WriteLine($"  Are Equal : {res.AreEqual}");
            if (!res.AreEqual)
                Console.WriteLine($"  Differences: {res.Differences}");
            Console.WriteLine();
        }
    }
}
