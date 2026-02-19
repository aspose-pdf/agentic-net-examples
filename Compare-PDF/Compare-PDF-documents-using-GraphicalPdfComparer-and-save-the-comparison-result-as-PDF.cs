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
            Console.WriteLine("Usage: ComparePdf <firstPdf> <secondPdf> <outputPdf>");
            return;
        }

        string firstPath = args[0];
        string secondPath = args[1];
        string outputPath = args[2];

        // Verify that the input files exist
        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"Error: File not found - {firstPath}");
            return;
        }
        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"Error: File not found - {secondPath}");
            return;
        }

        try
        {
            // Load the two PDF documents
            Document firstDoc = new Document(firstPath);
            Document secondDoc = new Document(secondPath);

            // Create the graphical comparer and optionally configure its properties
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();
            // comparer.Threshold = 0; // percentage of change to ignore (default 0%)
            // comparer.Resolution = 150; // DPI for the comparison images (default 150)
            // comparer.Color = Aspose.Pdf.Color.Red; // highlight color (default red)

            // Perform the comparison and save the result as a PDF
            comparer.CompareDocumentsToPdf(firstDoc, secondDoc, outputPath);

            Console.WriteLine($"Comparison PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}