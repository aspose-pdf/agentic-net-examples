using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string diffPdf = "diff.pdf";

        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        using (Document doc1 = new Document(firstPdf))
        using (Document doc2 = new Document(secondPdf))
        {
            // Default comparison options
            ComparisonOptions options = new ComparisonOptions();
            // Compare page by page and save the diff PDF
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, diffPdf);
        }

        Console.WriteLine($"Comparison PDF saved to '{diffPdf}'.");
    }
}