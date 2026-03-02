using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Directory containing the PDFs to compare
        const string directoryPath = "pdfs";

        // Input PDF file names (adjust as needed)
        string firstPdfPath = Path.Combine(directoryPath, "doc1.pdf");
        string secondPdfPath = Path.Combine(directoryPath, "doc2.pdf");

        // Path for the comparison result PDF
        string resultPdfPath = Path.Combine(directoryPath, "comparison_result.pdf");

        // Verify that both input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create default comparison options (customize if needed)
                ComparisonOptions options = new ComparisonOptions();

                // Perform page‑by‑page text comparison and save the visual result as PDF
                List<List<DiffOperation>> differences =
                    TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);

                Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
                Console.WriteLine($"Pages compared: {differences.Count}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}