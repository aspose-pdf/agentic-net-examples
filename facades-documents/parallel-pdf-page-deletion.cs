using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class ParallelPdfPageDeletion
{
    // Represents a single deletion job
    private class DeletionJob
    {
        public string InputPath { get; }
        public int[] PagesToDelete { get; }
        public string OutputPath { get; }

        public DeletionJob(string inputPath, int[] pagesToDelete, string outputPath)
        {
            InputPath = inputPath;
            PagesToDelete = pagesToDelete;
            OutputPath = outputPath;
        }
    }

    static void Main()
    {
        // Example list of jobs – replace with real data as needed
        var jobs = new List<DeletionJob>
        {
            new DeletionJob("doc1.pdf", new[] { 2, 3 }, "doc1_clean.pdf"),
            new DeletionJob("doc2.pdf", new[] { 1 }, "doc2_clean.pdf"),
            new DeletionJob("doc3.pdf", new[] { 5, 6, 7 }, "doc3_clean.pdf")
        };

        // Process all jobs in parallel
        Parallel.ForEach(jobs, job =>
        {
            try
            {
                // Verify input file exists before attempting deletion
                if (!File.Exists(job.InputPath))
                {
                    Console.Error.WriteLine($"Input file not found: {job.InputPath}");
                    return;
                }

                // Each parallel iteration creates its own PdfFileEditor instance
                // (PdfFileEditor is not thread‑safe, so we must not share it)
                PdfFileEditor editor = new PdfFileEditor();

                // Delete the specified pages and write the result to the output file
                bool success = editor.Delete(job.InputPath, job.PagesToDelete, job.OutputPath);

                if (success)
                {
                    Console.WriteLine($"Deleted pages {string.Join(",", job.PagesToDelete)} from '{job.InputPath}' → '{job.OutputPath}'");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to delete pages from '{job.InputPath}'");
                }

                // No need to call Close() – PdfFileEditor does not implement IDisposable
                // and does not hold unmanaged resources that require explicit release.
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{job.InputPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Parallel deletion completed.");
    }
}