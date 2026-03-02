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

        if (!File.Exists(docPath1))
        {
            Console.Error.WriteLine($"File not found: {docPath1}");
            return;
        }
        if (!File.Exists(docPath2))
        {
            Console.Error.WriteLine($"File not found: {docPath2}");
            return;
        }

        try
        {
            // Load the two PDFs using using blocks for deterministic disposal
            using (Document doc1 = new Document(docPath1))
            using (Document doc2 = new Document(docPath2))
            {
                // Create default comparison options
                ComparisonOptions options = new ComparisonOptions();

                // Compare the documents page by page and save the visual diff to a PDF file
                // The method returns a list of changes; we ignore it here
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