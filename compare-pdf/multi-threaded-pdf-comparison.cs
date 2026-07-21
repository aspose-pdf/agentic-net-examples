using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    // Entry point
    static void Main()
    {
        // Define PDF pairs to compare and their result files
        var comparisons = new List<(string FileA, string FileB, string Result)>
        {
            ("doc1_a.pdf", "doc1_b.pdf", "doc1_result.pdf"),
            ("doc2_a.pdf", "doc2_b.pdf", "doc2_result.pdf"),
            ("doc3_a.pdf", "doc3_b.pdf", "doc3_result.pdf")
        };

        // Create a task for each comparison
        var tasks = new List<Task>();
        foreach (var item in comparisons)
        {
            tasks.Add(Task.Run(() => CompareAndSave(item.FileA, item.FileB, item.Result)));
        }

        // Wait for all comparisons to finish
        Task.WaitAll(tasks.ToArray());

        Console.WriteLine("All PDF comparisons have completed.");
    }

    // Performs a side‑by‑side comparison of two PDFs and saves the result
    static void CompareAndSave(string pathA, string pathB, string resultPath)
    {
        // Verify input files exist
        if (!File.Exists(pathA) || !File.Exists(pathB))
        {
            Console.Error.WriteLine($"Missing input file(s) for comparison: '{pathA}' or '{pathB}'.");
            return;
        }

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document docA = new Document(pathA))
            using (Document docB = new Document(pathB))
            {
                // Create default comparison options (customize if needed)
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Perform the comparison; result is written to resultPath
                SideBySidePdfComparer.Compare(docA, docB, resultPath, options);
            }

            Console.WriteLine($"Compared '{Path.GetFileName(pathA)}' with '{Path.GetFileName(pathB)}' -> '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error comparing '{pathA}' and '{pathB}': {ex.Message}");
        }
    }
}