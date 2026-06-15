using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfBatchComparer
{
    // Entry point
    static void Main()
    {
        // Input: directory containing PDFs to compare
        const string sourceDirectory = @"C:\PdfBatch\Input";
        // Reference PDF to compare each file against
        const string referencePdfPath = @"C:\PdfBatch\Reference\baseline.pdf";
        // Output directory for comparison results
        const string outputDirectory = @"C:\PdfBatch\Output";

        // Validate paths
        if (!Directory.Exists(sourceDirectory))
        {
            Console.Error.WriteLine($"Source directory not found: {sourceDirectory}");
            return;
        }
        if (!File.Exists(referencePdfPath))
        {
            Console.Error.WriteLine($"Reference PDF not found: {referencePdfPath}");
            return;
        }
        Directory.CreateDirectory(outputDirectory);

        // Gather all PDF files in the source directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        // Load the reference document once (shared across tasks)
        using (Document referenceDoc = new Document(referencePdfPath))
        {
            // Prepare parallel options – limit degree of parallelism to avoid high memory usage
            ParallelOptions parallelOptions = new ParallelOptions {
                // Adjust this value based on available memory / CPU cores
                MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount / 2)
            };

            // Perform comparisons in parallel
            Parallel.ForEach(pdfFiles, parallelOptions, pdfPath =>
            {
                try
                {
                    // Load the target document inside its own using block
                    using (Document targetDoc = new Document(pdfPath))
                    {
                        // Prepare comparison options (default settings)
                        SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions();

                        // Build result file name: <original>_vs_baseline.pdf
                        string resultFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_vs_baseline.pdf";
                        string resultPath = Path.Combine(outputDirectory, resultFileName);

                        // Perform side‑by‑side comparison and save the result PDF
                        SideBySidePdfComparer.Compare(referenceDoc, targetDoc, resultPath, compareOptions);
                    }

                    Console.WriteLine($"Compared: {Path.GetFileName(pdfPath)}");
                }
                catch (Exception ex)
                {
                    // Log any errors but continue processing other files
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            });
        }

        Console.WriteLine("Batch comparison completed.");
    }
}