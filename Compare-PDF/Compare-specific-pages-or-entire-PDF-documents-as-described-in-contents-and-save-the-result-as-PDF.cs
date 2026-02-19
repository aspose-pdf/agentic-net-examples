using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - path to first PDF
        // args[1] - path to second PDF
        // args[2] - output PDF path
        // Optional args[3] - page number from first PDF (1‑based)
        // Optional args[4] - page number from second PDF (1‑based)

        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <firstPdf> <secondPdf> <outputPdf> [page1] [page2]");
            return;
        }

        string firstPdfPath = args[0];
        string secondPdfPath = args[1];
        string outputPdfPath = args[2];

        // Validate input files
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPdfPath}");
            return;
        }

        // Ensure output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPdfPath));
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the two PDF documents
            Document doc1 = new Document(firstPdfPath);
            Document doc2 = new Document(secondPdfPath);

            // If page numbers are supplied, compare those specific pages
            if (args.Length >= 5 &&
                int.TryParse(args[3], out int pageNum1) &&
                int.TryParse(args[4], out int pageNum2))
            {
                // Validate page numbers
                if (pageNum1 < 1 || pageNum1 > doc1.Pages.Count)
                {
                    Console.Error.WriteLine($"Error: page number {pageNum1} is out of range for the first document.");
                    return;
                }

                if (pageNum2 < 1 || pageNum2 > doc2.Pages.Count)
                {
                    Console.Error.WriteLine($"Error: page number {pageNum2} is out of range for the second document.");
                    return;
                }

                // Retrieve the pages (Aspose.Pdf uses 1‑based indexing)
                Page page1 = doc1.Pages[pageNum1];
                Page page2 = doc2.Pages[pageNum2];

                // Perform side‑by‑side page comparison
                SideBySidePdfComparer.Compare(page1, page2, outputPdfPath, new SideBySideComparisonOptions());

                Console.WriteLine($"Page comparison completed. Result saved to '{outputPdfPath}'.");
            }
            else
            {
                // Compare the whole documents
                SideBySidePdfComparer.Compare(doc1, doc2, outputPdfPath, new SideBySideComparisonOptions());

                Console.WriteLine($"Document comparison completed. Result saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}