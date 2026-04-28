using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: first PDF path, second PDF path, output folder.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <firstPdf> <secondPdf> <outputFolder>");
            return;
        }

        string firstPdfPath  = args[0];
        string secondPdfPath = args[1];
        string outputFolder   = args[2];

        // Validate input files and output folder.
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

        if (!Directory.Exists(outputFolder))
        {
            Console.Error.WriteLine($"Output folder does not exist: {outputFolder}");
            return;
        }

        // Build the result PDF path.
        string resultPdfPath = Path.Combine(outputFolder, "diff.pdf");

        try
        {
            // Load both documents inside using blocks for deterministic disposal.
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create the comparer and generate the diff PDF.
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Diff PDF saved to: {resultPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}