using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Input directory containing PDFs to compare
        string inputDirectory = args.Length > 0 ? args[0] : "InputPdfs";
        // Output directory for comparison results
        string outputDirectory = args.Length > 1 ? args[1] : "ComparisonResults";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory
        string[] allPdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (allPdfFiles.Length < 2)
        {
            Console.Error.WriteLine("At least two PDF files are required for comparison.");
            return;
        }

        // Use the first PDF as the baseline document
        string baselinePath = allPdfFiles[0];
        var filesToCompare = allPdfFiles.Skip(1).ToArray();

        // Limit concurrency to avoid excessive memory consumption
        var parallelOptions = new ParallelOptions
        {
            // Example: use half of the logical processors, but at least one
            MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount / 2)
        };

        Parallel.ForEach(filesToCompare, parallelOptions, targetPath =>
        {
            try
            {
                // Open both documents inside using blocks for deterministic disposal
                using (Aspose.Pdf.Document baselineDoc = new Aspose.Pdf.Document(baselinePath))
                using (Aspose.Pdf.Document targetDoc = new Aspose.Pdf.Document(targetPath))
                {
                    // Create default side‑by‑side comparison options
                    Aspose.Pdf.Comparison.SideBySideComparisonOptions compareOptions = new Aspose.Pdf.Comparison.SideBySideComparisonOptions();

                    // Build result file name
                    string resultFileName = Path.GetFileNameWithoutExtension(targetPath) + "_SideBySide.pdf";
                    string resultPath = Path.Combine(outputDirectory, resultFileName);

                    // Perform the comparison and save the result PDF
                    Aspose.Pdf.Comparison.SideBySidePdfComparer.Compare(baselineDoc, targetDoc, resultPath, compareOptions);

                    Console.WriteLine($"Compared '{Path.GetFileName(targetPath)}' -> '{resultFileName}'");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error comparing '{targetPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch comparison completed.");
    }
}