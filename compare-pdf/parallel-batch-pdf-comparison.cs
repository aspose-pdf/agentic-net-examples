using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Path to the reference PDF against which all others will be compared
        const string referencePath = "reference.pdf";

        // Directory containing the batch of PDFs to compare
        const string pdfDirectory = "BatchPdfs";

        // Directory where side‑by‑side comparison PDFs will be saved
        const string outputDir = "ComparisonResults";

        // Validate inputs
        if (!File.Exists(referencePath))
        {
            Console.Error.WriteLine($"Reference PDF not found: {referencePath}");
            return;
        }

        if (!Directory.Exists(pdfDirectory))
        {
            Console.Error.WriteLine($"PDF directory not found: {pdfDirectory}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(pdfDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        // Limit the number of concurrent tasks to avoid high memory usage
        ParallelOptions parallelOptions = new ParallelOptions {
            // Adjust this value based on the environment; 4 is a safe default
            MaxDegreeOfParallelism = 4
        };

        // Compare each PDF in the batch to the reference PDF
        Parallel.ForEach(pdfFiles, parallelOptions, pdfPath =>
        {
            try
            {
                // Load both documents inside using blocks for deterministic disposal
                using (Document referenceDoc = new Document(referencePath))
                using (Document targetDoc = new Document(pdfPath))
                {
                    // Set up comparison options (default configuration)
                    ComparisonOptions compareOptions = new ComparisonOptions();

                    // Perform a flat text comparison; returns a list of differences
                    List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(referenceDoc, targetDoc, compareOptions);

                    // Create a side‑by‑side visual comparison PDF
                    string resultPdfPath = Path.Combine(
                        outputDir,
                        $"{Path.GetFileNameWithoutExtension(pdfPath)}_vs_reference.pdf");

                    SideBySideComparisonOptions sideOptions = new SideBySideComparisonOptions(); // default options
                    SideBySidePdfComparer.Compare(referenceDoc, targetDoc, resultPdfPath, sideOptions);

                    // Output a simple summary for this file
                    Console.WriteLine($"Compared '{pdfPath}' – changes detected: {diffs?.Count ?? 0}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch PDF comparison completed.");
    }
}