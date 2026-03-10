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
        const string resultPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two PDF documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdf1Path))
            using (Document doc2 = new Document(pdf2Path))
            {
                // Create default comparison options (customize if needed)
                ComparisonOptions options = new ComparisonOptions();

                // Compare the documents page by page and save the visual diff to a PDF file
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}