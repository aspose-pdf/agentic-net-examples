using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Path to the directory containing the PDF files to compare.
        // You can pass the directory as a command‑line argument or set it here.
        string inputDirectory = args.Length > 0 ? args[0] : @"C:\PdfFiles";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        // Get all PDF files in the directory.
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        if (pdfFiles.Length < 2)
        {
            Console.Error.WriteLine("At least two PDF files are required for comparison.");
            return;
        }

        // Use the first two PDFs found for the comparison.
        string firstPdfPath  = pdfFiles[0];
        string secondPdfPath = pdfFiles[1];
        string resultPdfPath = Path.Combine(inputDirectory, "ComparisonResult.pdf");

        try
        {
            // Load the documents inside using blocks for deterministic disposal.
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create default comparison options.
                ComparisonOptions options = new ComparisonOptions();

                // Perform page‑by‑page comparison and save the visual result to a PDF.
                // The method returns a list of differences per page, which we ignore here.
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);
            }

            Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}