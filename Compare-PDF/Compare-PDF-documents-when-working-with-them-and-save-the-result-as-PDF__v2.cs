using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Configure side‑by‑side comparison options (defaults are fine, but we can set explicitly)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Example: show additional change marks across pages
                AdditionalChangeMarks = false,
                ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // Perform the comparison; the method writes the result directly to the specified file
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, options);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
    }
}