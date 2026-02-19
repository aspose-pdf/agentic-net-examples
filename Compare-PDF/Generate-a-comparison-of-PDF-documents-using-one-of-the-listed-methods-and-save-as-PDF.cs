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
            Console.Error.WriteLine("Usage: <program> <firstPdf> <secondPdf> <outputPdf>");
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
            Document firstDoc = new Document(firstPath);
            Document secondDoc = new Document(secondPath);

            // Configure side‑by‑side comparison options (default settings)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform the comparison; the result is written directly to outputPath
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPath, options);

            Console.WriteLine($"Comparison completed. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}