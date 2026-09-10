using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfTextComparison
{
    static void Main()
    {
        // Paths to the two PDF files that have the same content but different compression settings
        const string pdfPathA = "document_compressed_a.pdf";
        const string pdfPathB = "document_compressed_b.pdf";

        // Verify that both files exist before proceeding
        if (!File.Exists(pdfPathA))
        {
            Console.Error.WriteLine($"File not found: {pdfPathA}");
            return;
        }
        if (!File.Exists(pdfPathB))
        {
            Console.Error.WriteLine($"File not found: {pdfPathB}");
            return;
        }

        // Load the two PDF documents inside using blocks for deterministic disposal
        using (Document docA = new Document(pdfPathA))
        using (Document docB = new Document(pdfPathB))
        {
            // Create comparison options – default settings are sufficient for text comparison
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat (whole‑document) text comparison.
            // This method concatenates the text of all pages before comparing,
            // so differences caused only by compression are ignored.
            var diffOperations = TextPdfComparer.CompareFlatDocuments(docA, docB, options);

            // Output the result – number of textual differences found
            Console.WriteLine($"Textual differences detected: {diffOperations.Count}");

            // Iterate over the diff list and display details using the provided ToString() representation
            foreach (var diff in diffOperations)
            {
                Console.WriteLine(diff.ToString());
            }
        }
    }
}
