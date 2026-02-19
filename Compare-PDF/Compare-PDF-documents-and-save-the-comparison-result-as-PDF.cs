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
        // Output PDF path for the comparison result
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

            // Configure side‑by‑side comparison options (optional)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Example: show additional change marks
                AdditionalChangeMarks = true,
                // Comparison mode can be set if needed (default is IgnoreSpaces)
                // ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // Perform the comparison; the method writes the result to resultPdfPath
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, resultPdfPath, options);

            // Optionally, confirm that the result file was created
            if (File.Exists(resultPdfPath))
            {
                Console.WriteLine($"Comparison completed successfully. Result saved to '{resultPdfPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Comparison completed but the result file was not found.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}