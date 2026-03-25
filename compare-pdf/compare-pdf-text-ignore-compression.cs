using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfA = "original.pdf";      // PDF with default compression
        const string pdfB = "compressed.pdf";    // Same content, different compression
        const string resultPdf = "diff_result.pdf"; // PDF that visualises differences

        if (!File.Exists(pdfA) || !File.Exists(pdfB))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents – using blocks guarantee deterministic disposal
        using (Document doc1 = new Document(pdfA))
        using (Document doc2 = new Document(pdfB))
        {
            // Comparison options – default mode (Normal) is used; no ComparisonMode property exists in recent versions
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat (whole‑document) text comparison and save a visual diff PDF
            var diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPdf);

            Console.WriteLine($"Textual differences found: {diffs.Count}");
            foreach (var diff in diffs)
            {
                Console.WriteLine(diff);
            }
        }
    }
}
