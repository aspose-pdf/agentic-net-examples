using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string docPath1 = "document1.pdf";
        const string docPath2 = "document2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(docPath1))
        {
            Console.Error.WriteLine($"File not found: {docPath1}");
            return;
        }
        if (!File.Exists(docPath2))
        {
            Console.Error.WriteLine($"File not found: {docPath2}");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(docPath1))
            using (Document doc2 = new Document(docPath2))
            {
                // Create default comparison options (can be customized if needed)
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Perform side‑by‑side comparison and save the result PDF
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