using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the PDFs to compare and the output file
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDF documents
        Document doc1 = new Document(firstPdf);
        Document doc2 = new Document(secondPdf);

        // Comparison options – the Aspose.Pdf.Comparison.ComparisonOptions class does not expose an IgnoreCase flag.
        // Therefore we instantiate it without any case‑sensitivity settings.
        ComparisonOptions compOptions = new ComparisonOptions();

        // Side‑by‑side visual comparison (static class, cannot be instantiated)
        SideBySideComparisonOptions sideOptions = new SideBySideComparisonOptions();
        // The side‑by‑side comparer does not accept ComparisonOptions directly; it uses its own options class.
        // If you need a textual diff you can use TextPdfComparer with compOptions instead.
        SideBySidePdfComparer.Compare(doc1, doc2, outputPdf, sideOptions);

        Console.WriteLine($"Comparison completed. Result saved to '{outputPdf}'.");
    }
}
