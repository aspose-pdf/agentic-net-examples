using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";

        // Verify that both files exist before proceeding
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents (using statements ensure proper disposal)
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // ComparisonOptions no longer expose EnableFontComparison, EnableTextComparison, etc.
            // The default options already enable text (and therefore font) comparison.
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat document comparison. This method returns a list of
            // DiffOperation objects that describe each detected change.
            List<DiffOperation> diffOperations = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);

            // Count how many of the diff operations are related to font changes.
            int fontDiffCount = 0;
            foreach (DiffOperation diff in diffOperations)
            {
                // DiffOperation.Operation is an enum; its name contains "Font" for font‑related changes.
                if (diff.Operation.ToString().IndexOf("Font", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    fontDiffCount++;
                }
            }

            // Output the results.
            Console.WriteLine($"Total differences detected: {diffOperations.Count}");
            Console.WriteLine($"Font differences reported as separate operations: {fontDiffCount}");

            // Optionally, generate a PDF that visualizes the differences.
            const string resultPdfPath = "font_differences_report.pdf";
            TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPdfPath);
        }
    }
}
