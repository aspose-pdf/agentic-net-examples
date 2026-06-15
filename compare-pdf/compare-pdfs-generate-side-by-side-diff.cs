using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the PDF files to compare
        const string pdfPath1 = "doc1.pdf";
        const string pdfPath2 = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the documents using the core Document API (no Facades)
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create comparison options.
            // NOTE: The ComparisonOptions class does not expose an IgnoreCase property in the documented API.
            // If a newer version provides it, you can uncomment the line below.
            // options.IgnoreCase = true;
            ComparisonOptions options = new ComparisonOptions
            {
                // Example of setting other available options (optional)
                ExcludeTables = false,
                ExtractionArea = null,
                ExcludeAreas1 = null,
                ExcludeAreas2 = null,
                EditOperationsOrder = EditOperationsOrder.InsertFirst
            };

            // Perform a flat (whole‑document) text comparison.
            // The method returns a list of DiffOperation objects describing the changes.
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);

            // Output basic information about the comparison.
            Console.WriteLine($"Total differences found: {diffs.Count}");
            foreach (DiffOperation diff in diffs)
            {
                Console.WriteLine($"{diff.Operation}: \"{diff.Text}\"");
            }

            // Optionally generate a PDF that visualizes the differences.
            // This uses the side‑by‑side comparison API, which also accepts ComparisonOptions.
            SideBySideComparisonOptions sideOptions = new SideBySideComparisonOptions
            {
                // Use the same comparison mode as the flat comparison (default is IgnoreSpaces).
                // If you need case‑insensitive comparison and the API supports it,
                // set the appropriate property here.
                ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // Generate side‑by‑side PDF showing the differences.
            SideBySidePdfComparer.Compare(doc1, doc2, resultPath, sideOptions);
            Console.WriteLine($"Side‑by‑side comparison PDF saved to '{resultPath}'.");
        }
    }
}