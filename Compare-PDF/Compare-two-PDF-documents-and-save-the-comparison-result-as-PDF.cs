using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expect: <firstPdfPath> <secondPdfPath> <outputPdfPath>
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: program <firstPdf> <secondPdf> <outputPdf>");
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
            // Load the two PDF documents
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Configure side‑by‑side comparison options (customize as needed)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Example: show change marks that appear on other pages
                // AdditionalChangeMarks = true
            };

            // Perform comparison and save the result directly to the output file
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPdfPath, options);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Comparison failed: {ex.Message}");
        }
    }
}