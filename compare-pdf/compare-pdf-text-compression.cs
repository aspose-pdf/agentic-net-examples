using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfTextComparison
{
    static void Main()
    {
        // Paths to the two PDFs that have the same logical content but different compression settings
        const string pdfPathA = "document_compressed.pdf";
        const string pdfPathB = "document_uncompressed.pdf";
        const string diffResultPath = "text_diff_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdfPathA) || !File.Exists(pdfPathB))
        {
            Console.Error.WriteLine("One or both PDF files were not found.");
            return;
        }

        // Load the PDFs using the recommended using pattern (lifecycle rule)
        using (Document docA = new Document(pdfPathA))
        using (Document docB = new Document(pdfPathB))
        {
            // Configure comparison options – default options are sufficient for text comparison.
            // No need to set any extraction area; compression differences are ignored automatically.
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat (whole‑document) text comparison.
            // The overload that saves a PDF with visual diff is used to verify that only textual changes are reported.
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(
                docA,
                docB,
                options,
                diffResultPath
            );

            // Output the textual diff summary.
            Console.WriteLine($"Total textual differences detected: {diffs.Count}");
            foreach (DiffOperation diff in diffs)
            {
                Console.WriteLine($"- Operation: {diff.Operation}, Text: \"{diff.Text}\"");
            }

            // The diffResultPath PDF contains visual markers of the differences.
            Console.WriteLine($"Diff PDF saved to '{diffResultPath}'.");
        }
    }
}