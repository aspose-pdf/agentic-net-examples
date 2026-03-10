using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Directory containing the PDF files to compare
        const string directoryPath = "pdfs";
        // Path for the resulting comparison PDF
        const string resultPdfPath = "comparison_result.pdf";

        // Verify the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine($"Directory not found: {directoryPath}");
            return;
        }

        // Get all PDF files in the directory
        string[] pdfFiles = Directory.GetFiles(directoryPath, "*.pdf");
        if (pdfFiles.Length < 2)
        {
            Console.Error.WriteLine("At least two PDF files are required for comparison.");
            return;
        }

        // Load the first two PDF documents
        using (Document doc1 = new Document(pdfFiles[0]))
        using (Document doc2 = new Document(pdfFiles[1]))
        {
            // Create default comparison options
            ComparisonOptions options = new ComparisonOptions();

            // Compare the documents page by page and save the result as a PDF
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
    }
}