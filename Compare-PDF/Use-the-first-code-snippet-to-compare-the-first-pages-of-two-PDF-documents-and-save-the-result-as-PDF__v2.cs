using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPath    = "comparison.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Ensure each document has at least one page (Aspose.Pdf uses 1‑based indexing)
            if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
            {
                Console.Error.WriteLine("One of the PDFs does not contain any pages.");
                return;
            }

            // Retrieve the first page from each document
            Page page1 = doc1.Pages[1];
            Page page2 = doc2.Pages[1];

            // Create default side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform the comparison; the method writes the result directly to the file system
            SideBySidePdfComparer.Compare(page1, page2, resultPath, options);
        }

        Console.WriteLine($"Side‑by‑side comparison saved to '{resultPath}'.");
    }
}