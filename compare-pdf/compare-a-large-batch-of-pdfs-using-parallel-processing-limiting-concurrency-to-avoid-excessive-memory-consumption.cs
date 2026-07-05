using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class BatchPdfComparer
{
    // Entry point
    static void Main()
    {
        // Input directory containing PDFs to compare
        const string inputDirectory = @"C:\PdfBatch\Input";
        // Output directory for comparison results
        const string outputDirectory = @"C:\PdfBatch\Results";

        // Verify that the input directory exists; otherwise exit with a clear message
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: '{inputDirectory}'. Please create the directory and place PDF files to compare.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length < 2)
        {
            Console.Error.WriteLine("At least two PDF files are required for comparison.");
            return;
        }

        // Choose the first file as the reference document
        string referencePath = pdfFiles[0];
        string[] filesToCompare = new string[pdfFiles.Length - 1];
        Array.Copy(pdfFiles, 1, filesToCompare, 0, filesToCompare.Length);

        // Limit the degree of parallelism to avoid high memory usage
        ParallelOptions parallelOptions = new ParallelOptions
        {
            // Adjust this value based on available memory / CPU cores
            MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount / 2)
        };

        // Perform comparisons in parallel
        Parallel.ForEach(filesToCompare, parallelOptions, targetPath =>
        {
            try
            {
                // Load reference and target documents inside using blocks for deterministic disposal
                using (Document referenceDoc = new Document(referencePath))
                using (Document targetDoc = new Document(targetPath))
                {
                    // Prepare comparison options (default settings are sufficient for most cases)
                    SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions();

                    // Build result file name: <reference>_vs_<target>.pdf
                    string resultFileName = $"{Path.GetFileNameWithoutExtension(referencePath)}_vs_{Path.GetFileNameWithoutExtension(targetPath)}.pdf";
                    string resultPath = Path.Combine(outputDirectory, resultFileName);

                    // Perform side‑by‑side comparison; the method writes the result PDF directly
                    SideBySidePdfComparer.Compare(referenceDoc, targetDoc, resultPath, compareOptions);
                }

                Console.WriteLine($"Compared '{Path.GetFileName(targetPath)}' with reference. Result saved.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error comparing '{targetPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch comparison completed.");
    }
}
