using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "doc1.pdf";
        const string pdfPath2 = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both PDFs inside using blocks to ensure proper disposal.
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Set up comparison options (default values are sufficient for most scenarios).
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat document comparison.
            // This method returns a list of DiffOperation objects and also writes the visual
            // comparison result to the specified PDF file.
            var differences = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPath);

            Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
            Console.WriteLine($"Total differences detected: {differences?.Count ?? 0}");
        }
    }
}