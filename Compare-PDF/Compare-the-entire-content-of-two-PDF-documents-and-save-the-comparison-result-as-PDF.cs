using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the source PDFs and the output file
        string firstPdfPath = "first.pdf";
        string secondPdfPath = "second.pdf";
        string outputPdfPath = "comparison.pdf";

        // Verify that the input files exist
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

        // Load the two documents
        Document firstDoc = new Document(firstPdfPath);
        Document secondDoc = new Document(secondPdfPath);

        // Configure side‑by‑side comparison options (default settings are sufficient for a basic comparison)
        SideBySideComparisonOptions options = new SideBySideComparisonOptions();

        // Perform the comparison; the result is written directly to the specified output file
        SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPdfPath, options);

        Console.WriteLine($"Comparison PDF saved to: {outputPdfPath}");
    }
}