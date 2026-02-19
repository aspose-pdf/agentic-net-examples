using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonExample
{
    static void Main(string[] args)
    {
        // Input PDF file paths
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        // Output PDF file path
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that input files exist
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
            // Load the two documents
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Configure comparison options (you can adjust as needed)
            ComparisonOptions options = new ComparisonOptions
            {
                // Example: ignore spaces during text comparison
                // (uncomment the line below if you want to ignore spaces)
                // EditOperationsOrder = ComparisonMode.IgnoreSpaces
            };

            // Perform the comparison and save the result PDF
            // This overload saves the output directly to the specified path
            TextPdfComparer.CompareDocumentsPageByPage(firstDoc, secondDoc, options, resultPdfPath);

            Console.WriteLine($"Comparison completed successfully. Result saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}