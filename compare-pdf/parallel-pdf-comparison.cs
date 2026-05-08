using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonTask
{
    // Represents a pair of PDFs to compare and the output file for the result.
    public string FirstPath { get; }
    public string SecondPath { get; }
    public string ResultPath { get; }

    public PdfComparisonTask(string first, string second, string result)
    {
        FirstPath = first;
        SecondPath = second;
        ResultPath = result;
    }
}

class Program
{
    static void Main()
    {
        // Define the comparison jobs.
        var tasks = new List<PdfComparisonTask>
        {
            new PdfComparisonTask("doc1_a.pdf", "doc1_b.pdf", "doc1_result.pdf"),
            new PdfComparisonTask("doc2_a.pdf", "doc2_b.pdf", "doc2_result.pdf"),
            // Add more tasks as needed.
        };

        // Use a thread‑safe collection to store any errors that occur during processing.
        var errors = new ConcurrentBag<string>();

        // Run each comparison in parallel. Parallel.ForEach uses the thread pool and
        // automatically manages the number of concurrent threads.
        Parallel.ForEach(tasks, task =>
        {
            try
            {
                // Verify that input files exist before attempting to load them.
                if (!File.Exists(task.FirstPath))
                    throw new FileNotFoundException($"File not found: {task.FirstPath}");
                if (!File.Exists(task.SecondPath))
                    throw new FileNotFoundException($"File not found: {task.SecondPath}");

                // Load the two source PDFs inside using blocks for deterministic disposal.
                using (Document doc1 = new Document(task.FirstPath))
                using (Document doc2 = new Document(task.SecondPath))
                {
                    // Prepare comparison options (default settings are sufficient for a basic run).
                    var compareOptions = new SideBySideComparisonOptions();

                    // Perform the side‑by‑side comparison and write the result to the target PDF.
                    SideBySidePdfComparer.Compare(doc1, doc2, task.ResultPath, compareOptions);
                }

                Console.WriteLine($"Comparison completed: {task.ResultPath}");
            }
            catch (Exception ex)
            {
                // Capture any exception so that the main thread can report all failures after processing.
                errors.Add($"Task [{task.FirstPath}] vs [{task.SecondPath}] failed: {ex.Message}");
            }
        });

        // Report any errors that occurred during the parallel execution.
        if (errors.Count > 0)
        {
            Console.WriteLine("Some comparisons failed:");
            foreach (var err in errors)
                Console.WriteLine(err);
        }
        else
        {
            Console.WriteLine("All PDF comparisons completed successfully.");
        }
    }
}
