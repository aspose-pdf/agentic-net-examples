using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the two PDFs that have the same content but different compression settings
        const string pdfPathA = "document_compressed.pdf";
        const string pdfPathB = "document_uncompressed.pdf";
        const string diffResultPath = "text_diff_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdfPathA) || !File.Exists(pdfPathB))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks to ensure proper disposal
        using (Document docA = new Document(pdfPathA))
        using (Document docB = new Document(pdfPathB))
        {
            // Create default comparison options (no special settings required for text comparison)
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat text comparison – the pages are concatenated into a single text block
            // before the diff is calculated, so compression differences do not affect the result.
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(docA, docB, options, diffResultPath);

            // Output a simple summary of the diff operation
            Console.WriteLine($"Text comparison completed. Number of differences: {diffs.Count}");
            Console.WriteLine($"Detailed diff PDF saved to: {diffResultPath}");
        }
    }
}