using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string doc1Path = "document1.pdf";
        const string doc2Path = "document2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both source PDFs exist
        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Wrap both Document instances in using blocks for deterministic disposal
            using (Document doc1 = new Document(doc1Path))
            using (Document doc2 = new Document(doc2Path))
            {
                // Create comparison options (default settings are sufficient for a basic run)
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Optional: customize the highlight color for differences
                // options.DifferencesColor = Aspose.Pdf.Color.Red;

                // Perform side‑by‑side comparison and save the result as a new PDF
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