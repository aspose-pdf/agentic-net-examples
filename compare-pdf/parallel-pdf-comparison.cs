using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Define the PDF pairs to compare and the output file for each result.
        var comparisons = new List<(string file1, string file2, string result)>
        {
            ("doc1a.pdf", "doc1b.pdf", "result1.pdf"),
            ("doc2a.pdf", "doc2b.pdf", "result2.pdf")
            // Add additional pairs as needed.
        };

        // Verify that all source files exist before starting the tasks.
        foreach (var (f1, f2, _) in comparisons)
        {
            if (!File.Exists(f1) || !File.Exists(f2))
            {
                Console.Error.WriteLine($"Missing file: {f1} or {f2}");
                return;
            }
        }

        // Create a task for each comparison to run them in parallel.
        var tasks = new List<Task>();
        foreach (var (file1, file2, result) in comparisons)
        {
            tasks.Add(Task.Run(() =>
            {
                // Load each PDF inside a using block to ensure proper disposal.
                using (Document doc1 = new Document(file1))
                using (Document doc2 = new Document(file2))
                {
                    // Configure side‑by‑side comparison options (default settings are sufficient).
                    SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                    // Perform the comparison and save the side‑by‑side result PDF.
                    SideBySidePdfComparer.Compare(doc1, doc2, result, options);
                }

                Console.WriteLine($"Comparison saved to {result}");
            }));
        }

        // Wait for all comparison tasks to complete.
        Task.WaitAll(tasks.ToArray());

        Console.WriteLine("All PDF comparisons have finished.");
    }
}