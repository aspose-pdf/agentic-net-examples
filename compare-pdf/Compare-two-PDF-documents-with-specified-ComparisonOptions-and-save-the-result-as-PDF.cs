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

        // Output PDF path where the comparison result will be saved
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
                    // Example: do not exclude tables; you can adjust other properties here
                    ExcludeTables = false
                };

                // Perform page‑by‑page comparison and save the visual result to a PDF file.
                // This overload both returns the list of differences and writes the result PDF.
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);

                Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during loading or comparison
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}