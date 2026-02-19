using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: first PDF path, second PDF path, output PDF path
        if (args.Length != 3)
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
            // Load the two documents
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Prepare comparison options (default options are sufficient for a basic side‑by‑side comparison)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform the side‑by‑side comparison; the result is saved directly to outputPdfPath
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPdfPath, options);

            Console.WriteLine($"Comparison completed successfully. Result saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}