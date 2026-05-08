using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonExample
{
    static void Main()
    {
        // Input PDF file paths
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Output path for the diff PDF
        const string diffPdfPath = "diff_result.pdf";

        // Validate input files
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load the two documents and compare them page by page
        // Documents are wrapped in using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Default comparison options
            ComparisonOptions options = new ComparisonOptions();

            // Perform the comparison and save the diff PDF
            // This overload writes the visual diff directly to the specified file
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, diffPdfPath);
        }

        Console.WriteLine($"Comparison completed. Diff PDF saved to '{diffPdfPath}'.");
    }
}