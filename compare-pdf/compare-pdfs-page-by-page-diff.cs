using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdf1Path = "doc1.pdf";
        const string pdf2Path = "doc2.pdf";
        const string diffPath = "diff.pdf";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both PDFs inside using blocks to ensure proper disposal.
        using (Document doc1 = new Document(pdf1Path))
        using (Document doc2 = new Document(pdf2Path))
        {
            // Use default comparison options.
            ComparisonOptions options = new ComparisonOptions();

            // Compare the documents page by page and save the diff PDF.
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, diffPath);
        }

        Console.WriteLine($"Diff PDF saved to '{diffPath}'.");
    }
}