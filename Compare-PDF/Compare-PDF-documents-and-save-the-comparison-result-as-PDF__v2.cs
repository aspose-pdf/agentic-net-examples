using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPath  = "first.pdf";
        const string secondPath = "second.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"File not found: {firstPath}");
            return;
        }
        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"File not found: {secondPath}");
            return;
        }

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPath))
            using (Document doc2 = new Document(secondPath))
            {
                // Configure side‑by‑side comparison options
                SideBySideComparisonOptions options = new SideBySideComparisonOptions
                {
                    // Show change marks on pages where differences exist
                    AdditionalChangeMarks = true,
                    // Use the default comparison mode (ignore spaces)
                    ComparisonMode = ComparisonMode.IgnoreSpaces
                };

                // Perform the comparison and save the result as a PDF
                SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}