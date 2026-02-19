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
            Document firstDoc = new Document(firstPath);
            Document secondDoc = new Document(secondPath);

            // Configure side‑by‑side comparison options (optional)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                AdditionalChangeMarks = true // show change marks from other pages
            };

            // Perform comparison and save the result
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPath, options);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}