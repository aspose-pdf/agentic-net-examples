using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        // Output PDF path for the comparison result
        const string resultPdfPath = "comparison_result.pdf";

        // Validate input files exist
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
                // Create the graphical comparer
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Perform the comparison and save the result as a PDF
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
        }
        catch (ArgumentException ex)
        {
            // Thrown if pages differ in size or result path is invalid
            Console.Error.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}