using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string docPath1 = "doc1.pdf";
        const string docPath2 = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(docPath1) || !File.Exists(docPath2))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        using (Document doc1 = new Document(docPath1))
        using (Document doc2 = new Document(docPath2))
        {
            // Define rectangular areas to exclude from each document (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle[] excludeFromFirst = new Aspose.Pdf.Rectangle[]
            {
                new Aspose.Pdf.Rectangle(100, 500, 300, 600) // example area in first PDF
            };

            Aspose.Pdf.Rectangle[] excludeFromSecond = new Aspose.Pdf.Rectangle[]
            {
                new Aspose.Pdf.Rectangle(50, 400, 250, 550) // example area in second PDF
            };

            ComparisonOptions options = new ComparisonOptions
            {
                ExcludeAreas1 = excludeFromFirst,
                ExcludeAreas2 = excludeFromSecond
            };

            // Compare the documents page‑by‑page and save the visual diff PDF
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }
}