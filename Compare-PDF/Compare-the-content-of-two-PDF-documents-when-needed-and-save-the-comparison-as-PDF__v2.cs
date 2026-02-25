using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPath    = "comparison.pdf";

        // Verify that both input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create default side‑by‑side comparison options
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Optional: customize options, e.g. ignore spaces during text comparison
                // options.ComparisonMode = ComparisonMode.IgnoreSpaces;

                // Perform the comparison and save the result as a PDF
                SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
            }

            Console.WriteLine($"Comparison PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}