using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "doc1.pdf";
        const string secondPdfPath = "doc2.pdf";
        const string outputPath    = "comparison.pdf";

        // Verify that both input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Configure side‑by‑side comparison options (optional)
                var compareOptions = new SideBySideComparisonOptions
                {
                    // Example: ignore spaces when comparing text
                    // ComparisonMode = ComparisonMode.IgnoreSpaces
                };

                // Perform the comparison and save the result as a PDF
                SideBySidePdfComparer.Compare(doc1, doc2, outputPath, compareOptions);
            }

            Console.WriteLine($"Comparison PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}