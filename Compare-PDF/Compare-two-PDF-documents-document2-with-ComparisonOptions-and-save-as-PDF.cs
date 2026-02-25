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
        const string outputPath = "comparison_result.pdf";

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
            // Load the two PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(docPath1))
            using (Document doc2 = new Document(docPath2))
            {
                // Configure comparison options (adjust as needed)
                ComparisonOptions options = new ComparisonOptions
                {
                    // Example: ignore tables during comparison
                    ExcludeTables = false,
                    // Example: set an extraction area (optional)
                    // ExtractionArea = new Aspose.Pdf.Rectangle(0, 0, 500, 800)
                };

                // Perform page‑by‑page comparison and save the result as a PDF
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, outputPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}