using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparer
{
    static void Main()
    {
        const string firstPdf  = "doc1.pdf";
        const string secondPdf = "doc2.pdf";
        const string diffPdf   = "diff.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdf))
            using (Document doc2 = new Document(secondPdf))
            {
                // Create default comparison options
                ComparisonOptions options = new ComparisonOptions();

                // Compare page‑by‑page and save the diff PDF
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, diffPdf);
            }

            Console.WriteLine($"Comparison completed. Diff PDF saved to '{diffPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}