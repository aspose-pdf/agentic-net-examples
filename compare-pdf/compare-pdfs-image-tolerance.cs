using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPath = "first.pdf";
        const string secondPath = "second.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"File not found: {firstPath}");
            return;
        }
        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"File not found: {secondPath}");
            return;
        }

        try
        {
            using (Document doc1 = new Document(firstPath))
            using (Document doc2 = new Document(secondPath))
            {
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.Threshold = 5.0; // tolerance in percent
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
                Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}