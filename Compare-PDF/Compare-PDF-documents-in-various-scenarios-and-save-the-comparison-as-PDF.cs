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
            Console.WriteLine("Usage: <program> <firstPdf> <secondPdf> <outputPdf>");
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

            // Configure side‑by‑side comparison options
            SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions
            {
                AdditionalChangeMarks = true // show change marks from other pages
                // ComparisonMode can be set if needed, e.g. ComparisonMode.IgnoreSpaces
            };

            // Perform the comparison and save the result PDF
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPdfPath, compareOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF comparison: {ex.Message}");
        }
    }
}