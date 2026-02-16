using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expect: first PDF path, second PDF path, output PDF path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <firstPdf> <secondPdf> <outputPdf>");
            return;
        }

        string firstPdfPath = args[0];
        string secondPdfPath = args[1];
        string outputPdfPath = args[2];

        // Verify input files exist
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

        try
        {
            // Load the two PDF documents
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Create default side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform the comparison; the result is written directly to outputPdfPath
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPdfPath, options);

            Console.WriteLine($"Comparison PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}