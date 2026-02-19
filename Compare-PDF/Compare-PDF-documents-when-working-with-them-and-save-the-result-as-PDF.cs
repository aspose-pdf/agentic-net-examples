using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file paths
        const string firstPdfPath = "FirstDocument.pdf";
        const string secondPdfPath = "SecondDocument.pdf";

        // Output PDF file path
        const string resultPdfPath = "ComparisonResult.pdf";

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

            // Configure side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Show change marks that belong to other pages
                AdditionalChangeMarks = true,
                // Example: ignore spaces when comparing text
                ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // Perform the comparison; the result is written directly to resultPdfPath
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, resultPdfPath, options);

            // Confirmation message
            Console.WriteLine($"Comparison completed successfully. Result saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}