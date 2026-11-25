using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string diffJsonPath  = "diff.json";

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

        // Load the PDF documents
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Configure comparison options: exclude tables from the diff
            ComparisonOptions options = new ComparisonOptions
            {
                ExcludeTables = true
            };

            // Perform a text‑based comparison. The method returns a list of diff
            // operations for each page (IList<IList<DiffOperation>>).
            var pageDiffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Generate the JSON diff report.
            new JsonDiffOutputGenerator().GenerateOutput(pageDiffs, diffJsonPath);
        }

        Console.WriteLine($"Comparison completed. Diff saved to '{diffJsonPath}'.");
    }
}
