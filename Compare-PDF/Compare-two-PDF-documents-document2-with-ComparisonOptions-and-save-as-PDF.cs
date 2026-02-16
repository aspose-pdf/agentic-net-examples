using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file paths
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";
        // Output PDF file path
        const string outputPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"Error: File not found – {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"Error: File not found – {pdfPath2}");
            return;
        }

        try
        {
            // Load the two PDF documents
            Document doc1 = new Document(pdfPath1);
            Document doc2 = new Document(pdfPath2);

            // Configure comparison options (customize as needed)
            ComparisonOptions options = new ComparisonOptions
            {
                // Example: ignore spaces during comparison
                // (the enum is in Aspose.Pdf.Comparison namespace)
                // Note: ComparisonOptions does not expose a direct property for mode,
                // but you can set it via the SideBySideComparisonOptions if needed.
                // Here we keep defaults.
            };

            // Perform page‑by‑page comparison and save the result PDF
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, outputPath);

            Console.WriteLine($"Comparison completed successfully. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}