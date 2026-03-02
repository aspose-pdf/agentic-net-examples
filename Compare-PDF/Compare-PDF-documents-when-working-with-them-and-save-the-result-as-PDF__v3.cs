using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1   = "document1.pdf";
        const string pdfPath2   = "document2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Create default comparison options (can be customized if needed)
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Perform side‑by‑side comparison and write the result directly to a PDF file
                SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}