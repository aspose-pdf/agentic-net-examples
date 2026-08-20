using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both source PDFs exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Create default comparison options (can be customized as needed)
                ComparisonOptions options = new ComparisonOptions();

                // Perform a flat document comparison.
                // The method returns a list of differences and also writes a PDF with visual diff.
                List<DiffOperation> differences = TextPdfComparer.CompareFlatDocuments(
                    doc1, doc2, options, resultPdfPath);

                Console.WriteLine($"Comparison finished. Total differences: {differences.Count}");
                Console.WriteLine($"Result PDF saved to: {resultPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}