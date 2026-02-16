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
            Console.WriteLine("Usage: ComparePdf <firstPdf> <secondPdf> <outputPdf>");
            return;
        }

        string firstPdfPath = args[0];
        string secondPdfPath = args[1];
        string outputPdfPath = args[2];

        // Verify that input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPdfPath}");
            return;
        }

        try
        {
            // Load the two PDF documents
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Configure side‑by‑side comparison options (optional)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                AdditionalChangeMarks = true // show change marks that appear on other pages
            };

            // Perform the comparison; the result is written directly to outputPdfPath
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPdfPath, options);

            Console.WriteLine($"Comparison completed successfully. Result saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}