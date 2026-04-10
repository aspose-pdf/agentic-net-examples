using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonMultiThreaded
{
    // Represents a pair of PDFs to compare and the path for the result file.
    private class ComparisonJob
    {
        public string InputPath1 { get; }
        public string InputPath2 { get; }
        public string OutputPath { get; }

        public ComparisonJob(string input1, string input2, string output)
        {
            InputPath1 = input1;
            InputPath2 = input2;
            OutputPath = output;
        }
    }

    static void Main()
    {
        // Example list of comparison jobs.
        var jobs = new List<ComparisonJob>
        {
            new ComparisonJob("docA1.pdf", "docA2.pdf", "resultA.pdf"),
            new ComparisonJob("docB1.pdf", "docB2.pdf", "resultB.pdf"),
            new ComparisonJob("docC1.pdf", "docC2.pdf", "resultC.pdf")
        };

        // Validate that all source files exist before starting.
        foreach (var job in jobs)
        {
            if (!File.Exists(job.InputPath1))
                Console.Error.WriteLine($"File not found: {job.InputPath1}");
            if (!File.Exists(job.InputPath2))
                Console.Error.WriteLine($"File not found: {job.InputPath2}");
        }

        // Prepare comparison options (default settings are sufficient for a basic side‑by‑side compare).
        SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions();

        // Create a list to hold the running tasks.
        var tasks = new List<Task>();

        // Launch each comparison in its own task.
        foreach (var job in jobs)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    // Load each document inside its own using block to guarantee disposal.
                    using (Document doc1 = new Document(job.InputPath1))
                    using (Document doc2 = new Document(job.InputPath2))
                    {
                        // Perform the side‑by‑side comparison and save the result.
                        SideBySidePdfComparer.Compare(doc1, doc2, job.OutputPath, compareOptions);
                    }

                    Console.WriteLine($"Comparison completed: {job.OutputPath}");
                }
                catch (Exception ex)
                {
                    // Capture any errors for this specific job.
                    Console.Error.WriteLine($"Error comparing '{job.InputPath1}' and '{job.InputPath2}': {ex.Message}");
                }
            });

            tasks.Add(task);
        }

        // Wait for all comparison tasks to finish.
        Task.WaitAll(tasks.ToArray());

        Console.WriteLine("All PDF comparisons have finished.");
    }
}