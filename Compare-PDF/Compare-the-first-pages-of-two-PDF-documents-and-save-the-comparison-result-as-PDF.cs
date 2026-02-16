using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF files – adjust paths as needed
        string firstPdfPath = "first.pdf";
        string secondPdfPath = "second.pdf";

        // Output file for the side‑by‑side comparison result
        string outputPath = "comparison.pdf";

        // Verify that both source files exist
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
            // Load the two PDF documents
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Aspose.Pdf collections are 1‑based; get the first page of each document
            Page firstPage = firstDoc.Pages[1];
            Page secondPage = secondDoc.Pages[1];

            // Create default side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Compare the two pages and save the result as a new PDF
            SideBySidePdfComparer.Compare(firstPage, secondPage, outputPath, options);

            Console.WriteLine($"Comparison PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}