using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string docPath1 = "document1.pdf";
        const string docPath2 = "document2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(docPath1) || !File.Exists(docPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both PDFs with deterministic disposal
        using (Document doc1 = new Document(docPath1))
        using (Document doc2 = new Document(docPath2))
        {
            // Create default comparison options (customize if needed)
            ComparisonOptions options = new ComparisonOptions();

            // Compare the documents page‑by‑page and save the visual diff PDF
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
    }
}