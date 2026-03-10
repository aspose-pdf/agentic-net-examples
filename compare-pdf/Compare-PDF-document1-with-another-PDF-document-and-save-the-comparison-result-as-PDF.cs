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

        // Verify that both source files exist
        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(doc1Path))
            using (Document doc2 = new Document(doc2Path))
            {
                // Create default side‑by‑side comparison options
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Perform the comparison and save the resulting PDF
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