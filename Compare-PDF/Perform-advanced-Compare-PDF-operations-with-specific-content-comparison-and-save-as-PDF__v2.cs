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

        // Verify that both source files exist
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
                // Configure comparison options (customize as needed)
                ComparisonOptions options = new ComparisonOptions
                {
                    // Example: do not exclude tables from the comparison
                    ExcludeTables = false,
                    // Additional options can be set here, e.g. ExcludeAreas1, ExtractionArea, etc.
                };

                // Perform a page‑by‑page text comparison and save the visual result as a PDF
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);
            }

            Console.WriteLine($"Comparison PDF successfully saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}