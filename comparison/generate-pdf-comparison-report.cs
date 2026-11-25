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
        const string reportPdf = "comparison_report.pdf";

        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            using (Document doc1 = new Document(firstPdf))
            using (Document doc2 = new Document(secondPdf))
            {
                // GraphicalPdfComparer does NOT implement IDisposable, so instantiate it directly
                var comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToPdf(doc1, doc2, reportPdf);
            }

            Console.WriteLine($"Comparison report saved to '{reportPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
