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
            Console.Error.WriteLine("Usage: Program <first.pdf> <second.pdf> <output.pdf>");
            return;
        }

        string firstPath = args[0];
        string secondPath = args[1];
        string outputPath = args[2];

        // Verify that input files exist
        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"File not found: {firstPath}");
            return;
        }

        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"File not found: {secondPath}");
            return;
        }

        try
        {
            // Load the two PDF documents
            Document doc1 = new Document(firstPath);
            Document doc2 = new Document(secondPath);

            // Create the graphical comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional: customize comparer settings
            // comparer.Resolution = 200; // DPI
            // comparer.Threshold = 5;    // percent

            // Perform the comparison and generate a PDF with the differences
            comparer.CompareDocumentsToPdf(doc1, doc2, outputPath);

            Console.WriteLine($"Comparison result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}