using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: first PDF, second PDF, output PDF
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <program> <firstPdfPath> <secondPdfPath> <outputPdfPath>");
            return;
        }

        string firstPdfPath = args[0];
        string secondPdfPath = args[1];
        string outputPdfPath = args[2];

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
            // Load the two PDF documents
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Create the graphical comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional: customize comparer properties
            // comparer.Color = Color.Red;          // default flag color
            // comparer.Resolution = 150;           // DPI for comparison images
            // comparer.Threshold = 0;              // percentage threshold

            // Compare the documents and generate a PDF with the differences
            comparer.CompareDocumentsToPdf(firstDoc, secondDoc, outputPdfPath);

            Console.WriteLine($"Comparison result saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}