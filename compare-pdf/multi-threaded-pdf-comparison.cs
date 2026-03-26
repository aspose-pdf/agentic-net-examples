using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    // Entry point can be async for cleaner awaiting of tasks.
    static async Task Main(string[] args)
    {
        // Base directory of the executable – helps to resolve relative paths reliably.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Define pairs of PDF files to compare (relative to the base directory).
        string[] firstFiles  = new string[] { "doc1a.pdf", "doc2a.pdf" };
        string[] secondFiles = new string[] { "doc1b.pdf", "doc2b.pdf" };

        if (firstFiles.Length != secondFiles.Length)
        {
            Console.WriteLine("The number of source files does not match the number of target files.");
            return;
        }

        List<Task> tasks = new List<Task>();
        for (int i = 0; i < firstFiles.Length; i++)
        {
            // Resolve full paths once – avoids the closure‑capture pitfall and makes debugging easier.
            string firstPath  = Path.GetFullPath(Path.Combine(baseDir, firstFiles[i]));
            string secondPath = Path.GetFullPath(Path.Combine(baseDir, secondFiles[i]));
            string resultPath = Path.GetFullPath(Path.Combine(baseDir, $"result_{i + 1}.pdf"));

            // Validate that the input files exist before queuing the work.
            if (!File.Exists(firstPath))
            {
                Console.WriteLine($"Input file not found: {firstPath}");
                continue; // Skip this pair and continue with the rest.
            }
            if (!File.Exists(secondPath))
            {
                Console.WriteLine($"Input file not found: {secondPath}");
                continue;
            }

            // Queue the comparison work on the thread‑pool.
            Task comparisonTask = Task.Run(() =>
            {
                // Each task works with its own local variables – no race conditions.
                using (Document doc1 = new Document(firstPath))
                using (Document doc2 = new Document(secondPath))
                {
                    SideBySideComparisonOptions options = new SideBySideComparisonOptions();
                    // The static Compare method writes the result directly to the supplied path.
                    SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
                }
                Console.WriteLine($"Comparison {i + 1} completed: {resultPath}");
            });

            tasks.Add(comparisonTask);
        }

        // Await all comparison tasks.
        await Task.WhenAll(tasks);
        Console.WriteLine("All PDF comparisons have been completed.");
    }
}
