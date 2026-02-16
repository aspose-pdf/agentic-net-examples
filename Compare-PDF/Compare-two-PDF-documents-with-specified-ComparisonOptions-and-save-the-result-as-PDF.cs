using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonDemo
{
    static void Main(string[] args)
    {
        // Input PDF file paths
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        // Output PDF file path
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPdfPath}");
            return;
        }

        try
        {
            // Load the two PDF documents
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Configure comparison options (customize as needed)
            ComparisonOptions options = new ComparisonOptions
            {
                // Example: ignore spaces during comparison
                // (the default mode is IgnoreSpaces)
                // EditOperationsOrder can be set if required
            };

            // Perform page‑by‑page text comparison and save the result
            // The static method writes the output directly to the specified file.
            TextPdfComparer.CompareDocumentsPageByPage(firstDoc, secondDoc, options, resultPdfPath);

            Console.WriteLine($"Comparison completed successfully. Result saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}