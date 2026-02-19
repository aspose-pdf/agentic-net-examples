using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
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
            // Load the two documents to be compared
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Create an instance of GraphicalPdfComparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional: customize comparer properties
            // comparer.Color = Aspose.Pdf.Color.Red;      // default is red
            // comparer.Resolution = 150;                  // default 150 dpi
            // comparer.Threshold = 0;                    // default 0%

            // Perform graphical comparison and generate a PDF with the differences
            comparer.CompareDocumentsToPdf(firstDoc, secondDoc, resultPdfPath);

            // The CompareDocumentsToPdf method already saves the result,
            // but if you need to explicitly save a Document instance, you could do:
            // {DocumentVar}.Save({OutputPath});
            Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}