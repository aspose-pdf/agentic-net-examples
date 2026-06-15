using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonMultiThreaded
{
    // Represents a pair of PDFs to compare and the target result file.
    private class ComparisonJob
    {
        public string FirstPdfPath { get; }
        public string SecondPdfPath { get; }
        public string ResultPdfPath { get; }

        public ComparisonJob(string first, string second, string result)
        {
            FirstPdfPath = first;
            SecondPdfPath = second;
            ResultPdfPath = result;
        }
    }

    static void Main()
    {
        // Prepare a list of comparison jobs.
        var jobs = new List<ComparisonJob>
        {
            new ComparisonJob("doc1_a.pdf", "doc1_b.pdf", "doc1_comparison.pdf"),
            new ComparisonJob("doc2_a.pdf", "doc2_b.pdf", "doc2_comparison.pdf"),
            // Add more jobs as needed.
        };

        // Collection to hold any errors that occur during processing.
        var errors = new List<string>();

        // Run each comparison in its own task (thread‑pool thread).
        var tasks = new List<Task>();
        foreach (var job in jobs)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    // Verify that source files exist before attempting to open them.
                    if (!File.Exists(job.FirstPdfPath))
                        throw new FileNotFoundException($"File not found: {job.FirstPdfPath}");
                    if (!File.Exists(job.SecondPdfPath))
                        throw new FileNotFoundException($"File not found: {job.SecondPdfPath}");

                    // Load both documents inside using blocks for deterministic disposal.
                    using (Document doc1 = new Document(job.FirstPdfPath))
                    using (Document doc2 = new Document(job.SecondPdfPath))
                    {
                        // Create default comparison options.
                        SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions();

                        // Perform side‑by‑side comparison and save the result PDF.
                        SideBySidePdfComparer.Compare(doc1, doc2, job.ResultPdfPath, compareOptions);
                    }

                    Console.WriteLine($"Comparison completed: {job.ResultPdfPath}");
                }
                catch (Exception ex)
                {
                    // Capture any exception so the main thread can report it.
                    lock (errors)
                    {
                        errors.Add($"Error processing {job.FirstPdfPath} & {job.SecondPdfPath}: {ex.Message}");
                    }
                }
            });

            tasks.Add(task);
        }

        // Wait for all comparison tasks to finish.
        Task.WaitAll(tasks.ToArray());

        // Report any errors that occurred.
        if (errors.Count > 0)
        {
            Console.WriteLine("The following errors were encountered:");
            foreach (var err in errors)
                Console.WriteLine(err);
        }
        else
        {
            Console.WriteLine("All PDF comparisons completed successfully.");
        }
    }
}