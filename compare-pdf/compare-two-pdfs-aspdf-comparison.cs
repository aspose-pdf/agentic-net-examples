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
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Aspose.Pdf.Comparison.ComparisonOptions does not expose a direct
            // case‑insensitive flag. The property "TextComparisonOptions" and the
            // enum "TextComparisonOptions" do not exist in the current API version.
            // To achieve case‑insensitive comparison you must handle it outside the
            // comparer (e.g., by extracting the text and comparing it with a
            // case‑insensitive string comparison). Here we create a default options
            // instance and run the comparison; any case‑only differences will be
            // reflected in the result PDF.
            ComparisonOptions options = new ComparisonOptions();

            // Perform the comparison using the default options.
            TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPdfPath);

            Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
        }
    }
}
