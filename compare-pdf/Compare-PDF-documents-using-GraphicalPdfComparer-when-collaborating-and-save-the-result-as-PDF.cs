using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the source PDFs and the result PDF
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create the comparer (default constructor)
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Optional: customize comparer properties
                // comparer.Color = Aspose.Pdf.Color.Red;
                // comparer.Resolution = 200;
                // comparer.Threshold = 5; // percent

                // Perform the graphical comparison and save the result as a PDF
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors (e.g., mismatched page sizes, I/O issues)
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}