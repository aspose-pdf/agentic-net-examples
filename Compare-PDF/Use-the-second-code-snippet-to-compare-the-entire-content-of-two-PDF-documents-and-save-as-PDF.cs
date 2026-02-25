using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string docPath1 = "doc1.pdf";
        const string docPath2 = "doc2.pdf";
        // Output path for the comparison result PDF
        const string resultPath = "comparison_result.pdf";

        // Verify that both input files exist
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
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(docPath1))
            using (Document doc2 = new Document(docPath2))
            {
                // Create the graphical comparer and compare the entire documents
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}