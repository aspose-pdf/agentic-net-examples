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
            ComparisonOptions options = new ComparisonOptions();
            // Default options are used; customize if needed.

            // Compare the documents page by page and save the diff PDF.
            TextPdfComparer.CompareDocumentsPageByPage(document1, document2, options, resultPath);
        }

        Console.WriteLine($"Comparison completed. Diff PDF saved as '{resultPath}'.");
    }
}