using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

public class Program
{
    public static void Main()
    {
        const string firstPdf = "original.pdf";
        const string secondPdf = "compressed.pdf";
        const string resultPdf = "diffResult.pdf";

        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        using (Document document1 = new Document(firstPdf))
        using (Document document2 = new Document(secondPdf))
        {
            // ComparisonOptions no longer exposes CompareMode or IgnoreImages.
            // Text comparison is enabled by default, and image differences are ignored
            // when only textual diff is required.
            ComparisonOptions options = new ComparisonOptions();

            List<DiffOperation> differences = TextPdfComparer.CompareFlatDocuments(document1, document2, options, resultPdf);
            Console.WriteLine($"Textual differences found: {differences.Count}");
        }
    }
}
