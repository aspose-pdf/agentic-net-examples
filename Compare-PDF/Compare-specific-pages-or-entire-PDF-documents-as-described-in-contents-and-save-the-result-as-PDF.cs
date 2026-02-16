using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - path to first PDF
        // 1 - path to second PDF
        // 2 - output PDF path
        // 3 - (optional) page number in first PDF
        // 4 - (optional) page number in second PDF
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <firstPdf> <secondPdf> <outputPdf> [pageNumber1] [pageNumber2]");
            return;
        }

        string firstPath = args[0];
        string secondPath = args[1];
        string outputPath = args[2];

        // Verify input files exist
        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"File not found: {firstPath}");
            return;
        }
        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"File not found: {secondPath}");
            return;
        }

        try
        {
            // Load the two PDF documents
            Document doc1 = new Document(firstPath);
            Document doc2 = new Document(secondPath);

            // Create default side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            if (args.Length >= 5)
            {
                // Compare specific pages
                if (int.TryParse(args[3], out int pageNum1) && int.TryParse(args[4], out int pageNum2))
                {
                    // Validate page numbers (Aspose.Pdf uses 1‑based indexing)
                    if (pageNum1 < 1 || pageNum1 > doc1.Pages.Count ||
                        pageNum2 < 1 || pageNum2 > doc2.Pages.Count)
                    {
                        Console.Error.WriteLine("Invalid page numbers supplied.");
                        return;
                    }

                    Page page1 = doc1.Pages[pageNum1];
                    Page page2 = doc2.Pages[pageNum2];

                    // Perform side‑by‑side comparison for the selected pages
                    SideBySidePdfComparer.Compare(page1, page2, outputPath, options);
                }
                else
                {
                    Console.Error.WriteLine("Page numbers must be valid integers.");
                    return;
                }
            }
            else
            {
                // Compare the entire documents
                SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
            }

            Console.WriteLine($"Comparison result saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}