using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdf1Path = "doc1.pdf";
        const string pdf2Path = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdf1Path))
        using (Document doc2 = new Document(pdf2Path))
        {
            // Create a graphical comparer and set a custom tolerance (percentage)
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();
            comparer.Threshold = 5.0; // Ignore differences smaller than 5%

            // Perform the comparison and save the result as a PDF
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }
}