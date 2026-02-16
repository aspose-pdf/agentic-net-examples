using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfSideBySideComparison
{
    static void Main(string[] args)
    {
        // Input PDF file paths (adjust as needed)
        const string firstPdfPath = "FirstDocument.pdf";
        const string secondPdfPath = "SecondDocument.pdf";
        const string resultPdfPath = "ComparisonResult.pdf";

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
            // Load the two PDF documents
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Configure side‑by‑side comparison options (default values are fine for a basic run)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Example: enable additional change marks (optional)
                AdditionalChangeMarks = true,
                // Comparison mode can be set if needed, e.g., ComparisonMode.IgnoreSpaces
                // ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // Perform the comparison; the method creates a new PDF document internally
            // The result is saved directly to the specified output path
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, resultPdfPath, options);

            // If you need to work with the resulting document object, you can load it afterwards:
            // Document resultDoc = new Document(resultPdfPath);
            // resultDoc.Save(resultPdfPath); // using the provided document-save rule

            Console.WriteLine($"Comparison completed successfully. Result saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}