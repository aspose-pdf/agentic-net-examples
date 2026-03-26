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
        const string resultPath = "diff.pdf";

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

        using (Document document1 = new Document(firstPath))
        using (Document document2 = new Document(secondPath))
        {
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();
            comparer.CompareDocumentsToPdf(document1, document2, resultPath);
        }

        Console.WriteLine($"Visual diff PDF saved to '{resultPath}'.");
    }
}