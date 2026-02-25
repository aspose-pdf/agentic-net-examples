using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Directory containing the PDF files to compare
        const string directoryPath = @"C:\PdfFiles";

        if (!Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine($"Directory not found: {directoryPath}");
            return;
        }

        // Get all PDF files in the directory (sorted for deterministic pairing)
        string[] pdfFiles = Directory.GetFiles(directoryPath, "*.pdf")
                                     .OrderBy(f => f)
                                     .ToArray();

        if (pdfFiles.Length < 2)
        {
            Console.Error.WriteLine("At least two PDF files are required for comparison.");
            return;
        }

        // Use the first two PDFs for the comparison
        string firstPdfPath  = pdfFiles[0];
        string secondPdfPath = pdfFiles[1];
        string resultPath    = Path.Combine(directoryPath, "ComparisonResult.pdf");

        try
        {
            // Load the two documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Configure comparison options (default settings are sufficient for a basic run)
                ComparisonOptions options = new ComparisonOptions();

                // Perform page‑by‑page comparison and save the result as a PDF
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);
            }

            Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}