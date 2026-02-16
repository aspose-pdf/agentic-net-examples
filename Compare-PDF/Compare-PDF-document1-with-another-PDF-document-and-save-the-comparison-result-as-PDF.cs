using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file paths
        string firstPdfPath = "document1.pdf";
        string secondPdfPath = "document2.pdf";

        // Output PDF path for the comparison result
        string outputPdfPath = "comparison_result.pdf";

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
            // Load the two documents to be compared
            Document doc1 = new Document(firstPdfPath);
            Document doc2 = new Document(secondPdfPath);

            // Configure side‑by‑side comparison options (optional settings can be added)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Example: ignore spaces when comparing text
                // ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // Perform the comparison; the method saves the result directly to the specified path
            SideBySidePdfComparer.Compare(doc1, doc2, outputPdfPath, options);

            Console.WriteLine($"Comparison PDF successfully saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}