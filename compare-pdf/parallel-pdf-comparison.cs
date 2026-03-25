using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string referencePdfPath = "reference.pdf"; // PDF to compare against
        const string inputFolder      = "input-pdfs";   // Folder with PDFs to compare
        const string outputFolder     = "comparison-results";
        const int maxConcurrency      = 4;               // Adjust to limit memory usage

        if (!File.Exists(referencePdfPath))
        {
            Console.Error.WriteLine($"Reference PDF not found: {referencePdfPath}");
            return;
        }
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxConcurrency };

        Parallel.ForEach(pdfFiles, parallelOptions, pdfPath =>
        {
            // Skip the reference file if it resides in the same folder
            if (string.Equals(pdfPath, referencePdfPath, StringComparison.OrdinalIgnoreCase))
                return;

            try
            {
                using (Document reference = new Document(referencePdfPath))
                using (Document target    = new Document(pdfPath))
                {
                    string resultFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_compare.pdf";
                    string resultPath     = Path.Combine(outputFolder, resultFileName);

                    SideBySideComparisonOptions options = new SideBySideComparisonOptions();
                    // Example option – show differences in red (default)
                    // options.DiffColor = Aspose.Pdf.Color.Red;

                    SideBySidePdfComparer.Compare(reference, target, resultPath, options);
                    Console.WriteLine($"Compared '{Path.GetFileName(pdfPath)}' → '{resultFileName}'");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }
}