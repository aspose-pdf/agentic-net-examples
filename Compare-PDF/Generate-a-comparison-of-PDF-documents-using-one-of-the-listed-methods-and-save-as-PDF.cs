using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <firstPdfPath> <secondPdfPath> <outputPdfPath>
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <firstPdfPath> <secondPdfPath> <outputPdfPath>");
            return;
        }

        string firstPdfPath = args[0];
        string secondPdfPath = args[1];
        string outputPdfPath = args[2];

        // Verify that input files exist
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
            // Load the two PDF documents
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Configure side‑by‑side comparison options
            SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions
            {
                // Example setting: ignore spaces during text comparison
                ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // Perform the comparison; the result is saved directly to outputPdfPath
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPdfPath, compareOptions);

            Console.WriteLine($"Comparison completed successfully. Result saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during PDF comparison: {ex.Message}");
        }
    }
}