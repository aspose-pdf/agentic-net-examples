using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class FontComparisonDemo
{
    static void Main()
    {
        // Paths to the two PDF files that contain embedded fonts
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";

        // Verify that the input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files were not found.");
            return;
        }

        // Load the two PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure comparison options.
            // The PdfNonSpecificationFlags property is not available in the current API version,
            // so we rely on the default behaviour which already detects font name mismatches.
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat (whole‑document) text comparison.
            // This method combines the text of all pages before comparing,
            // which ensures that font changes are reported as separate diff operations.
            List<DiffOperation> diffOperations = TextPdfComparer.CompareFlatDocuments(
                doc1,
                doc2,
                options
            );

            // Output the total number of diff operations detected.
            Console.WriteLine($"Total diff operations: {diffOperations.Count}");

            // Verify that font differences are reported as distinct operations.
            // DiffOperation does not expose a Description property; use ToString() which contains
            // a readable description of the operation (including the operation type).
            int fontDiffCount = 0;
            foreach (DiffOperation diff in diffOperations)
            {
                string diffText = diff.ToString();
                if (!string.IsNullOrEmpty(diffText) &&
                    diffText.IndexOf("font", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    fontDiffCount++;
                }
            }

            Console.WriteLine($"Font‑related diff operations: {fontDiffCount}");

            // Optionally, generate a JSON report of the differences.
            if (diffOperations.Count > 0)
            {
                JsonDiffOutputGenerator jsonGenerator = new JsonDiffOutputGenerator();
                string jsonReportPath = "diff_report.json";
                jsonGenerator.GenerateOutput(diffOperations, jsonReportPath);
                Console.WriteLine($"JSON diff report saved to '{jsonReportPath}'.");
            }
        }
    }
}
