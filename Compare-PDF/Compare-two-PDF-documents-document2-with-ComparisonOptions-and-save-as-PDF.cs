using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Initialize comparison options (default settings)
        ComparisonOptions options = new ComparisonOptions();

        // Load both PDFs inside using blocks to ensure proper disposal
        using (Document doc1 = new Document(doc1Path))
        using (Document doc2 = new Document(doc2Path))
        {
            // Compare the documents page by page and save the diff as a PDF
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
    }
}