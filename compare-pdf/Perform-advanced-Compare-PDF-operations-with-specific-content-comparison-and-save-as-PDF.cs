using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the source PDFs and the output comparison PDF
        const string firstPdfPath  = "doc1.pdf";
        const string secondPdfPath = "doc2.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks to ensure proper disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create and configure side‑by‑side comparison options.
            // Adjust properties as needed (e.g., colors, page margins, etc.).
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Example customizations (uncomment if the properties exist in your version):
                // DeletionColor = Aspose.Pdf.Color.Red,
                // InsertionColor = Aspose.Pdf.Color.Green,
                // PageMargin = 20
            };

            // Perform the comparison. The method writes the result directly to the target PDF file.
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, options);
        }

        Console.WriteLine($"Side‑by‑side comparison PDF saved to '{resultPdfPath}'.");
    }
}