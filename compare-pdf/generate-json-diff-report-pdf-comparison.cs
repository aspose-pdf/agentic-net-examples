using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files to compare
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Path where the JSON diff report will be saved
        const string jsonReportPath = "diff_report.json";

        // Validate input files
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
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Comparison options – defaults are sufficient for text comparison
                ComparisonOptions options = new ComparisonOptions();

                // Perform a flat (whole‑document) text comparison
                List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);

                // Iterate over the diff operations and output them to the console
                foreach (DiffOperation diff in diffs)
                {
                    // DiffOperation provides a useful ToString implementation
                    Console.WriteLine(diff.ToString());
                    // Alternatively you can access the explicit properties:
                    // Console.WriteLine($"{diff.Operation} – \"{diff.Text}\"");
                }

                // Generate a JSON report of the differences
                JsonDiffOutputGenerator jsonGenerator = new JsonDiffOutputGenerator();
                // This overload writes the JSON directly to the specified file
                jsonGenerator.GenerateOutput(diffs, jsonReportPath);

                Console.WriteLine($"JSON diff report saved to '{jsonReportPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}