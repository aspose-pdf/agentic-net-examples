using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string doc1Path = "scanned1.pdf";
        const string doc2Path = "scanned2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        using (Document doc1 = new Document(doc1Path))
        using (Document doc2 = new Document(doc2Path))
        {
            // Set a custom tolerance (percentage) for image differences.
            GraphicalPdfComparer comparer = new GraphicalPdfComparer
            {
                Threshold = 5.0 // ignore changes smaller than 5%
            };

            // Perform graphical comparison and save the result as a PDF.
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }
}