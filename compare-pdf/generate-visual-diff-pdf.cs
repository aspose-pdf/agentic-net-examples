using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class VisualDiffGenerator
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "visual_diff.pdf";

        // Verify input files exist
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
            // Load the two documents inside using blocks (lifecycle rule)
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create the comparer (default constructor)
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Perform graphical comparison and save the result PDF
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Visual diff PDF created at '{resultPdfPath}'.");
        }
        catch (ArgumentException ex)
        {
            // Thrown if pages have different sizes or result path is invalid
            Console.Error.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}