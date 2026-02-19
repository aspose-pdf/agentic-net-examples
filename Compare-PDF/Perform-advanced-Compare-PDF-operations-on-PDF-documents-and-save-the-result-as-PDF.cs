using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF files – adjust paths as needed
        string firstPdfPath = "first.pdf";
        string secondPdfPath = "second.pdf";

        // Output files for the different comparison results
        string sideBySideOutput = "SideBySideComparisonResult.pdf";
        string textComparisonOutput = "TextComparisonResult.pdf";

        // Verify that both source files exist
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

            // ------------------------------------------------------------
            // 1. Side‑by‑side visual comparison (pages interleaved)
            // ------------------------------------------------------------
            SideBySideComparisonOptions sideOptions = new SideBySideComparisonOptions
            {
                // Example option – show change marks that appear on other pages
                AdditionalChangeMarks = true
                // Other properties (ComparisonArea1, ComparisonMode, etc.) can be set here
            };

            // The static Compare method creates the result PDF and saves it to the specified path
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, sideBySideOutput, sideOptions);
            Console.WriteLine($"Side‑by‑side comparison saved to: {sideBySideOutput}");

            // ------------------------------------------------------------
            // 2. Text‑only comparison with detailed diff statistics
            // ------------------------------------------------------------
            ComparisonOptions textOptions = new ComparisonOptions
            {
                // Example: ignore tables during text comparison
                ExcludeTables = false
                // Other options (ExcludeAreas1, ExtractionArea, etc.) can be configured here
            };

            // Perform page‑by‑page text comparison and save the result PDF
            TextPdfComparer.CompareDocumentsPageByPage(firstDoc, secondDoc, textOptions, textComparisonOutput);
            Console.WriteLine($"Text comparison saved to: {textComparisonOutput}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during PDF comparison: {ex.Message}");
        }
    }
}