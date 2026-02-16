using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonProgram
{
    static void Main(string[] args)
    {
        // Expect three arguments: first PDF, second PDF, output PDF
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: PdfComparisonProgram <first.pdf> <second.pdf> <output.pdf>");
            return;
        }

        string firstPath = args[0];
        string secondPath = args[1];
        string outputPath = args[2];

        // Verify input files exist
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
            Document firstDoc = new Document(firstPath);
            Document secondDoc = new Document(secondPath);

            // Configure side‑by‑side comparison options (default values are fine)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Example: show additional change marks
                AdditionalChangeMarks = false,
                ComparisonMode = ComparisonMode.Normal
            };

            // Perform the comparison and save the result
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPath, options);

            Console.WriteLine($"Comparison completed. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}