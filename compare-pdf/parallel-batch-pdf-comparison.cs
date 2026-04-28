using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to compare
        const string inputFolder = "InputPdfs";
        // Folder where comparison result PDFs will be saved
        const string outputFolder = "ComparisonResults";
        // Reference PDF filename (located inside the input folder)
        const string referencePdfFileName = "reference.pdf";

        // Ensure input and output directories exist
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Full path to the reference PDF
        string referencePdfPath = Path.Combine(inputFolder, referencePdfFileName);
        if (!File.Exists(referencePdfPath))
        {
            Console.WriteLine($"Reference PDF not found at '{referencePdfPath}'." );
            return;
        }

        // Collect all PDF files in the input folder (excluding the reference file)
        string[] allPdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        string[] pdfFiles = Array.FindAll(allPdfFiles, p =>
            !string.Equals(Path.GetFullPath(p), Path.GetFullPath(referencePdfPath), StringComparison.OrdinalIgnoreCase));

        // Parallel options to limit concurrency (e.g., 4 concurrent tasks)
        ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 4 };

        Parallel.ForEach(pdfFiles, parallelOptions, pdfPath =>
        {
            // Load the reference and the target PDF inside using blocks for deterministic disposal
            using (Document referenceDoc = new Document(referencePdfPath))
            using (Document targetDoc = new Document(pdfPath))
            {
                // Default comparison options (can be customized if needed)
                ComparisonOptions compareOptions = new ComparisonOptions();

                // Build result file name based on the target PDF name
                string resultFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_compare.pdf";
                string resultPath = Path.Combine(outputFolder, resultFileName);

                // Perform the comparison and save the visual diff PDF
                // This overload saves the result directly to the specified path
                TextPdfComparer.CompareFlatDocuments(referenceDoc, targetDoc, compareOptions, resultPath);
            }
        });

        Console.WriteLine("Batch PDF comparison completed.");
    }
}
