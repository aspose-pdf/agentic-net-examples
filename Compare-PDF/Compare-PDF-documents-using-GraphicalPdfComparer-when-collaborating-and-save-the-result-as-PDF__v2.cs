using System;
using System.IO;
using Aspose.Pdf;                       // Core PDF API
using Aspose.Pdf.Comparison;           // GraphicalPdfComparer resides here

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string firstPdfPath  = "FirstDocument.pdf";
        const string secondPdfPath = "SecondDocument.pdf";
        // Output PDF that will contain the visual comparison result
        const string resultPdfPath = "ComparisonResult.pdf";

        // Verify that the source files exist
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

        try
        {
            // Load the two documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create an instance of the graphical comparer
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Optional: customize appearance of differences
                // comparer.Color = Aspose.Pdf.Color.Red;      // default is red
                // comparer.Resolution = 150;                  // default 150 dpi
                // comparer.Threshold = 0;                    // default 0%

                // Perform the comparison and write the result directly to a PDF file
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
        }
        catch (ArgumentException argEx)
        {
            // Thrown if pages have different sizes or result path is invalid
            Console.Error.WriteLine($"Argument error: {argEx.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}