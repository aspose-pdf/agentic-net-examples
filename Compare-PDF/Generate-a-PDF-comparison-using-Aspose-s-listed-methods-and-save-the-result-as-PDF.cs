using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Paths to the PDFs to compare and the output file
        string firstPdfPath = "first.pdf";
        string secondPdfPath = "second.pdf";
        string outputPdfPath = "comparison_result.pdf";

        // Verify that the input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {secondPdfPath}");
            return;
        }

        try
        {
            // Load the two PDF documents
            Document doc1 = new Document(firstPdfPath);
            Document doc2 = new Document(secondPdfPath);

            // Create default side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform the comparison; the result is saved to outputPdfPath
            SideBySidePdfComparer.Compare(doc1, doc2, outputPdfPath, options);

            Console.WriteLine($"Comparison PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF comparison: {ex.Message}");
        }
    }
}