using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class BatchPdfComparer
{
    static void Main()
    {
        // Base directory of the application (works for both console and VS debugging)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Path to the reference PDF against which all others will be compared
        string referencePath = Path.Combine(baseDir, "reference.pdf");
        if (!File.Exists(referencePath))
        {
            Console.Error.WriteLine($"Reference PDF not found at '{referencePath}'. Execution stopped.");
            return;
        }

        // Directory containing the PDFs to be compared
        string inputDir = Path.Combine(baseDir, "InputPdfs");
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory '{inputDir}' not found. Falling back to current working directory.");
            inputDir = Directory.GetCurrentDirectory();
        }
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDir}'. Execution stopped.");
            return;
        }

        // Directory where comparison result PDFs will be saved
        string outputDir = Path.Combine(baseDir, "ComparisonResults");
        Directory.CreateDirectory(outputDir);

        // Maximum number of concurrent comparisons (adjust based on memory constraints)
        const int maxConcurrency = 4;
        SemaphoreSlim semaphore = new SemaphoreSlim(maxConcurrency);

        var tasks = new List<Task>();

        foreach (string file in pdfFiles)
        {
            // Skip the reference file itself if it appears in the list
            if (string.Equals(Path.GetFullPath(file), Path.GetFullPath(referencePath), StringComparison.OrdinalIgnoreCase))
                continue;

            // Queue a comparison task
            tasks.Add(Task.Run(async () =>
            {
                await semaphore.WaitAsync();
                try
                {
                    try
                    {
                        CompareAndSave(referencePath, file, outputDir);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error comparing '{Path.GetFileName(file)}': {ex.Message}");
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }));
        }

        // Wait for all comparisons to finish
        Task.WaitAll(tasks.ToArray());

        Console.WriteLine("Batch PDF comparison completed.");
    }

    static void CompareAndSave(string referencePath, string targetPath, string outputDir)
    {
        // Load the reference and target PDFs using Aspose.Pdf's Document class
        using (Document referenceDoc = new Document(referencePath))
        using (Document targetDoc = new Document(targetPath))
        {
            // Default comparison options; customize if needed
            ComparisonOptions options = new ComparisonOptions();

            // Construct a result file name that identifies the compared pair
            string resultFileName = $"{Path.GetFileNameWithoutExtension(targetPath)}_vs_{Path.GetFileNameWithoutExtension(referencePath)}.pdf";
            string resultPath = Path.Combine(outputDir, resultFileName);

            // Perform side‑by‑side comparison and let the API write the result PDF
            SideBySidePdfComparer.Compare(referenceDoc, targetDoc, resultPath, new SideBySideComparisonOptions { });
        }
    }
}
