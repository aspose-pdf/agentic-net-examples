using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the source PDF files and the comparison result
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that the input files exist
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
            // Load both PDF documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create an instance of the graphical comparer
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Perform the comparison and save the visual diff to a PDF file
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
        }
        catch (ArgumentException ex)
        {
            // Thrown if the pages have different sizes or the result path is invalid
            Console.Error.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Generic error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}