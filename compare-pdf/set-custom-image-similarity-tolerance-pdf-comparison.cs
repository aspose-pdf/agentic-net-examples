using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "original.pdf";
        const string pdfPath2 = "scanned.pdf";
        const string outputPath = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDF documents
        Document doc1 = new Document(pdfPath1);
        Document doc2 = new Document(pdfPath2);

        // Configure graphical comparer with a custom image similarity tolerance.
        // Threshold is expressed as a percentage of allowed difference.
        // For 85% similarity required, allow 15% difference.
        GraphicalPdfComparer comparer = new GraphicalPdfComparer
        {
            Threshold = 15.0 // tolerance in percent
        };

        // Perform visual side‑by‑side comparison and save the result PDF.
        comparer.CompareDocumentsToPdf(doc1, doc2, outputPath);

        Console.WriteLine($"Comparison completed. Result saved to '{outputPath}'.");
    }
}
