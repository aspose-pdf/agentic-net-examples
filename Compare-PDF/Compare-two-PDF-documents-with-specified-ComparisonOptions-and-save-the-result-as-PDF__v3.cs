using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        // Output PDF that will contain the comparison result
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that input files exist
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
            // Load the two documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Configure comparison options as needed
                ComparisonOptions options = new ComparisonOptions
                {
                    // Example: exclude tables from comparison
                    ExcludeTables = false,
                    // Example: set an extraction area (optional)
                    // ExtractionArea = new Aspose.Pdf.Rectangle(0, 0, 500, 800)
                };

                // Perform page‑by‑page comparison and save the visual result to a PDF file
                // The overload with a result path automatically writes the diff PDF.
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);

                Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}