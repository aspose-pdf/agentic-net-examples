using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expect a directory path as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: ComparePdfDir <directoryPath>");
            return;
        }

        string directoryPath = args[0];

        if (!Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine($"Error: Directory not found – {directoryPath}");
            return;
        }

        // Find PDF files in the directory (take the first two for comparison)
        string[] pdfFiles = Directory.GetFiles(directoryPath, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length < 2)
        {
            Console.Error.WriteLine("Error: Need at least two PDF files in the specified directory.");
            return;
        }

        string firstPdfPath = pdfFiles[0];
        string secondPdfPath = pdfFiles[1];

        // Validate files
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("Error: One of the PDF files does not exist.");
            return;
        }

        try
        {
            // Load the two documents
            Document doc1 = new Document(firstPdfPath);
            Document doc2 = new Document(secondPdfPath);

            // Set comparison options (default options are fine for a basic run)
            ComparisonOptions options = new ComparisonOptions();

            // Define output file path
            string outputPath = Path.Combine(directoryPath, "ComparisonResult.pdf");

            // Perform page‑by‑page comparison and save the result
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, outputPath);

            Console.WriteLine($"Comparison completed. Result saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}