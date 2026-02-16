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
            Console.Error.WriteLine("Usage: <program> <first.pdf> <second.pdf> <output.pdf>");
            return;
        }

        string firstPath = args[0];
        string secondPath = args[1];
        string outputPath = args[2];

        // Verify input files exist
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

            // Configure side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                AdditionalChangeMarks = true,
                // Example: ignore spaces during comparison
                // ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // Perform comparison; the result is saved directly to outputPath
            SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);

            Console.WriteLine($"Comparison completed. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF comparison: {ex.Message}");
        }
    }
}