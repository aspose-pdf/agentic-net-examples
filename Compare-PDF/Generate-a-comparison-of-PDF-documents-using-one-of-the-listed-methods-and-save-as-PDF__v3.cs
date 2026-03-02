using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        // Path for the comparison result PDF
        const string resultPdfPath = "comparison_result.pdf";

        // Validate input files
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        try
        {
            // Load the first document
            using (Document doc1 = new Document(firstPdfPath))
            // Load the second document
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create comparison options (default settings)
                ComparisonOptions options = new ComparisonOptions();

                // Perform page‑by‑page text comparison and save the result as PDF
                // This static method both returns the diff list and writes the PDF.
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);

                Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}