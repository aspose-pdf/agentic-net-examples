using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <firstPdf> <secondPdf> <outputPdf>
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: Program <firstPdf> <secondPdf> <outputPdf>");
            return;
        }

        string firstPath = args[0];
        string secondPath = args[1];
        string outputPath = args[2];

        // Verify that the input files exist
        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"Error: File not found – {firstPath}");
            return;
        }

        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPath}");
            return;
        }

        try
        {
            // Load the two PDF documents
            Document doc1 = new Document(firstPath);
            Document doc2 = new Document(secondPath);

            // Create the graphical comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional: configure comparer properties (e.g., threshold, resolution, color)
            // comparer.Threshold = 5; // ignore differences below 5%
            // comparer.Resolution = 200; // DPI for generated images
            // comparer.Color = Aspose.Pdf.Color.Yellow; // change flag color

            // Perform the comparison and save the result as a PDF
            comparer.CompareDocumentsToPdf(doc1, doc2, outputPath);

            Console.WriteLine($"Comparison PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}