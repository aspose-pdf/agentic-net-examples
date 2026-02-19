using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF files
        string firstPdfPath = "first.pdf";
        string secondPdfPath = "second.pdf";

        // Output PDF that will contain the visual differences
        string outputPdfPath = "diff.pdf";

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
            // Load the two documents to be compared
            Document doc1 = new Document(firstPdfPath);
            Document doc2 = new Document(secondPdfPath);

            // Create the graphical comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional: customize comparer properties
            // comparer.Color = Aspose.Pdf.Color.Red;   // default is red
            // comparer.Resolution = 150;               // default DPI
            // comparer.Threshold = 0;                  // default percentage

            // Perform the comparison and save the result as a PDF
            comparer.CompareDocumentsToPdf(doc1, doc2, outputPdfPath);

            Console.WriteLine($"Comparison PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}