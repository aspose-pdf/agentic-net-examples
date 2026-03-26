using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "diff.pdf";

        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        using (Document firstDoc = new Document(firstPdfPath))
        using (Document secondDoc = new Document(secondPdfPath))
        {
            ComparisonOptions options = new ComparisonOptions();
            // Form field values are compared by default; no extra flag is required.
            // Optionally define the order of edit operations.
            options.EditOperationsOrder = EditOperationsOrder.InsertFirst;

            // Perform the comparison and save the visual diff PDF.
            TextPdfComparer.CompareDocumentsPageByPage(firstDoc, secondDoc, options, resultPdfPath);

            Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
        }
    }
}