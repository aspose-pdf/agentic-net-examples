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
                // Configure comparison options as needed
                ComparisonOptions cmpOptions = new ComparisonOptions
                {
                    // Example: do not exclude tables from comparison
                    ExcludeTables = false,
                    // Additional options can be set here, e.g.:
                    // ExcludeAreas1 = new List<Aspose.Pdf.Rectangle>(),
                    // ExcludeAreas2 = new List<Aspose.Pdf.Rectangle>(),
                    // ExtractionArea = new Aspose.Pdf.Rectangle(0, 0, 500, 800)
                };

                // Perform page‑by‑page comparison and save the result as a PDF
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, cmpOptions, resultPdfPath);
            }

            Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}