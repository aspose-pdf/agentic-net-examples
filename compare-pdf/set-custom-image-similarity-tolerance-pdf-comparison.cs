using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfAPath = "scanned_a.pdf";
        const string pdfBPath = "scanned_b.pdf";
        const string diffOutputPath = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(pdfAPath))
        {
            Console.Error.WriteLine($"File not found: {pdfAPath}");
            return;
        }
        if (!File.Exists(pdfBPath))
        {
            Console.Error.WriteLine($"File not found: {pdfBPath}");
            return;
        }

        // Load the PDFs
        Document docA = new Document(pdfAPath);
        Document docB = new Document(pdfBPath);

        // Configure graphical comparison with a custom image similarity tolerance.
        // Threshold is expressed as a percentage of allowed visual difference.
        // For example, a threshold of 15 means up to 15% difference is tolerated (≈85% similarity).
        var graphicalComparer = new GraphicalPdfComparer
        {
            Threshold = 15.0 // tolerance in percent
        };

        // Perform the comparison and generate a diff PDF
        graphicalComparer.CompareDocumentsToPdf(docA, docB, diffOutputPath);

        Console.WriteLine($"Comparison completed. Result saved to '{diffOutputPath}'.");
    }
}
