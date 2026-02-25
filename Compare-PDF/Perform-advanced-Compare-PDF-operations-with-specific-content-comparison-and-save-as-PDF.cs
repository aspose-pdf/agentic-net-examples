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

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Configure side‑by‑side comparison options
                SideBySideComparisonOptions options = new SideBySideComparisonOptions
                {
                    // Show change marks that appear on other pages as well
                    AdditionalChangeMarks = true,
                    // Ignore whitespace differences for a cleaner diff
                    ComparisonMode = ComparisonMode.IgnoreSpaces,
                    // Do not exclude tables from the comparison
                    ExcludeTables = false
                };

                // Perform the comparison and save the result as a PDF
                SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, options);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}