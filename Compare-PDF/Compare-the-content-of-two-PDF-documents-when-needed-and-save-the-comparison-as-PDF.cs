using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file paths (provide via command‑line or use defaults)
        string firstPdfPath = args.Length > 0 ? args[0] : "first.pdf";
        string secondPdfPath = args.Length > 1 ? args[1] : "second.pdf";
        // Output PDF path for the comparison result
        string outputPdfPath = args.Length > 2 ? args[2] : "comparison.pdf";

        // Verify that the source files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPdfPath}");
            return;
        }

        try
        {
            // Load the two documents to be compared
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Configure side‑by‑side comparison options (default values are fine)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Example: show additional change marks (optional)
                AdditionalChangeMarks = false,
                ComparisonMode = ComparisonMode.Normal
            };

            // Perform the comparison; the result is written directly to outputPdfPath
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPdfPath, options);

            Console.WriteLine($"Comparison completed. Result saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}