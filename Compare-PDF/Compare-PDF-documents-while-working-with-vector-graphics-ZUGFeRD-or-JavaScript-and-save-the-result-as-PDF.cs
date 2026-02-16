using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files – can contain vector graphics, ZUGFeRD data, JavaScript, etc.
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Output PDF that will contain the side‑by‑side comparison result
        const string outputPdfPath = "comparison.pdf";

        // Verify that the source files exist before proceeding
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
            Document doc1 = new Document(firstPdfPath);   // load rule
            Document doc2 = new Document(secondPdfPath); // load rule

            // Configure side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Show additional change marks (e.g., marks that appear on other pages)
                AdditionalChangeMarks = true,

                // Use a comparison mode that ignores whitespace differences
                ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // Perform the comparison; the result is written directly to outputPdfPath
            SideBySidePdfComparer.Compare(doc1, doc2, outputPdfPath, options);

            // Confirmation message
            Console.WriteLine($"Comparison PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Generic error handling – reports any unexpected issues
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}