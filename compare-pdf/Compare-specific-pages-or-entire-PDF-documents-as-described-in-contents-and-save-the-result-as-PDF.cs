using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonExample
{
    static void Main()
    {
        // Input PDF file paths
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        // Path where the comparison result will be saved
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both source files exist
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
            // Load the first document inside a using block for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            // Load the second document inside a nested using block
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create default comparison options (can be customized if needed)
                ComparisonOptions options = new ComparisonOptions();

                // Perform a page‑by‑page text comparison and save the visual diff to a PDF
                // The method returns a list of changes per page; we ignore it here.
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}