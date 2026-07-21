using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class FontDifferenceComparer
{
    static void Main()
    {
        // Paths to the PDFs to compare
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string jsonReportPath = "fontDifferences.json";

        // Verify that both files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Configure comparison options – default options already enable text (and font) comparison
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat (whole‑document) comparison; returns a list of diff operations
            List<DiffOperation> diffOperations = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);

            // Filter and display only font‑related differences
            Console.WriteLine("Font differences found:");
            foreach (DiffOperation diff in diffOperations)
            {
                // Convert the enum value to string before searching for the word "Font"
                string operationText = diff.Operation.ToString();
                if (operationText.IndexOf("Font", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Use the built‑in ToString() which contains operation details such as page number and description
                    Console.WriteLine($"- {diff}");
                }
            }

            // Optionally generate a JSON report containing all diff operations
            JsonDiffOutputGenerator jsonGenerator = new JsonDiffOutputGenerator();
            jsonGenerator.GenerateOutput(diffOperations, jsonReportPath);
            Console.WriteLine($"JSON report written to '{jsonReportPath}'.");
        }
    }
}
