using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ComparisonResult
{
    public string FileA { get; set; }
    public string FileB { get; set; }
    public bool AreEqual { get; set; }
    public string Differences { get; set; }
}

class PdfComparer
{
    // Compare two PDFs: page count and full text content
    private static ComparisonResult Compare(string pathA, string pathB)
    {
        ComparisonResult result = new ComparisonResult {
            FileA = pathA,
            FileB = pathB,
            AreEqual = true,
            Differences = string.Empty
        };

        // Load first document
        using (Document docA = new Document(pathA))
        // Load second document
        using (Document docB = new Document(pathB))
        {
            // Compare page counts
            if (docA.Pages.Count != docB.Pages.Count)
            {
                result.AreEqual = false;
                result.Differences += $"Page count differs (A:{docA.Pages.Count} vs B:{docB.Pages.Count}). ";
            }

            // Extract full text from both documents
            string textA = ExtractFullText(docA);
            string textB = ExtractFullText(docB);

            if (!string.Equals(textA, textB, StringComparison.Ordinal))
            {
                result.AreEqual = false;
                result.Differences += "Text content differs. ";
            }
        }

        return result;
    }

    // Helper: extract all text from a document using TextAbsorber
    private static string ExtractFullText(Document doc)
    {
        TextAbsorber absorber = new TextAbsorber();
        // Use default TextExtractionOptions (pure text)
        doc.Pages.Accept(absorber);
        return absorber.Text ?? string.Empty;
    }

    // Run comparisons in parallel and collect results
    public static List<ComparisonResult> CompareInParallel(List<(string, string)> pairs)
    {
        var results = new ConcurrentBag<ComparisonResult>();
        var tasks = new List<Task>();

        foreach (var (fileA, fileB) in pairs)
        {
            // Capture variables for the task
            string a = fileA;
            string b = fileB;

            tasks.Add(Task.Run(() =>
            {
                var res = Compare(a, b);
                results.Add(res);
            }));
        }

        // Wait for all tasks to finish
        Task.WaitAll(tasks.ToArray());

        // Return as a regular list
        return new List<ComparisonResult>(results);
    }

    // Example usage
    static void Main()
    {
        // Define PDF pairs to compare
        var pdfPairs = new List<(string, string)>
        {
            ("doc1_v1.pdf", "doc1_v2.pdf"),
            ("report_A.pdf", "report_B.pdf"),
            ("sample1.pdf", "sample2.pdf")
        };

        // Validate files exist before starting
        foreach (var (a, b) in pdfPairs)
        {
            if (!File.Exists(a) || !File.Exists(b))
            {
                Console.Error.WriteLine($"Missing file: {(File.Exists(a) ? b : a)}");
                return;
            }
        }

        // Perform parallel comparison
        List<ComparisonResult> comparisonResults = CompareInParallel(pdfPairs);

        // Output results
        foreach (var res in comparisonResults)
        {
            Console.WriteLine($"[{res.FileA}] vs [{res.FileB}] => Equal: {res.AreEqual}");
            if (!res.AreEqual)
                Console.WriteLine($"  Differences: {res.Differences}");
        }
    }
}