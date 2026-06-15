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
        const string diffPdfPath   = "diff.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Default comparison options
                ComparisonOptions options = new ComparisonOptions();

                // Perform page‑by‑page comparison and save the diff PDF
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, diffPdfPath);
            }

            Console.WriteLine($"Diff PDF saved to '{diffPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}