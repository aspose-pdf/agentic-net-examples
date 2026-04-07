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
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Use default comparison options
        ComparisonOptions options = new ComparisonOptions();

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdf))
        using (Document doc2 = new Document(secondPdf))
        {
            // Compare page by page and save the visual diff PDF
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, diffPdf);
        }

        Console.WriteLine($"Comparison completed. Diff PDF saved to '{diffPdf}'.");
    }
}