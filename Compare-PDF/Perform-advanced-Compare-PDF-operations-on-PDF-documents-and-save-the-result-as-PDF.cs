using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(doc1Path))
            using (Document doc2 = new Document(doc2Path))
            {
                // Configure side‑by‑side comparison options
                SideBySideComparisonOptions options = new SideBySideComparisonOptions
                {
                    // Show change marks that exist on other pages as well
                    AdditionalChangeMarks = true,
                    // Normal comparison mode (spaces are taken into account)
                    ComparisonMode = ComparisonMode.Normal
                };

                // Perform the comparison; the result is saved directly to resultPath
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