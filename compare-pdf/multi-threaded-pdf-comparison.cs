using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    // Represents a pair of PDF files to be compared.
    private class PdfPair
    {
        public string File1 { get; }
        public string File2 { get; }
        public string ResultPath { get; }

        public PdfPair(string file1, string file2, string resultPath)
        {
            File1 = file1;
            File2 = file2;
            ResultPath = resultPath;
        }
    }

    static void Main()
    {
        // Example input: list of PDF file pairs.
        var pairs = new List<PdfPair>
        {
            new PdfPair("docA1.pdf", "docA2.pdf", "resultA.pdf"),
            new PdfPair("docB1.pdf", "docB2.pdf", "resultB.pdf"),
            new PdfPair("docC1.pdf", "docC2.pdf", "resultC.pdf")
        };

        // Validate that all source files exist before starting.
        foreach (var p in pairs)
        {
            if (!File.Exists(p.File1))
                Console.Error.WriteLine($"Source file not found: {p.File1}");
            if (!File.Exists(p.File2))
                Console.Error.WriteLine($"Source file not found: {p.File2}");
        }

        // Create a list of tasks, each performing a comparison in its own thread.
        var tasks = new List<Task>();

        foreach (var pair in pairs)
        {
            // Capture the current pair for the lambda.
            var currentPair = pair;

            var task = Task.Run(() =>
            {
                try
                {
                    // Load the first document.
                    using (Document doc1 = new Document(currentPair.File1))
                    // Load the second document.
                    using (Document doc2 = new Document(currentPair.File2))
                    {
                        // Configure side‑by‑side comparison options (default settings are fine).
                        SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                        // Perform the comparison; the result is written directly to the target PDF file.
                        SideBySidePdfComparer.Compare(doc1, doc2, currentPair.ResultPath, options);
                    }

                    Console.WriteLine($"Comparison completed: {currentPair.ResultPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error comparing '{currentPair.File1}' and '{currentPair.File2}': {ex.Message}");
                }
            });

            tasks.Add(task);
        }

        // Wait for all comparison tasks to finish.
        Task.WaitAll(tasks.ToArray());

        Console.WriteLine("All comparisons finished.");
    }
}