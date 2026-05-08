using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the two PDFs that have identical content but different compression settings
        const string pdfPathA = "document_compressed_a.pdf";
        const string pdfPathB = "document_compressed_b.pdf";
        const string diffResultPdf = "text_diff_result.pdf";

        // Verify that both files exist before proceeding
        if (!File.Exists(pdfPathA) || !File.Exists(pdfPathB))
        {
            Console.Error.WriteLine("One or both PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks to ensure deterministic disposal
        using (Document docA = new Document(pdfPathA))
        using (Document docB = new Document(pdfPathB))
        {
            // Configure comparison options – default settings compare text only
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat (whole‑document) text comparison.
            // This method concatenates the text of all pages before comparing,
            // so differences caused only by compression are ignored.
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(docA, docB, options, diffResultPdf);

            // Output a simple summary of the textual differences
            Console.WriteLine($"Total textual differences found: {diffs.Count}");
            foreach (DiffOperation diff in diffs)
            {
                // Use the correct members of DiffOperation (Operation and Text)
                Console.WriteLine($"- {diff.Operation} – \"{diff.Text}\"");
            }

            // The diffResultPdf file contains a visual representation of the differences.
            Console.WriteLine($"Diff PDF saved to '{diffResultPdf}'.");
        }
    }
}
