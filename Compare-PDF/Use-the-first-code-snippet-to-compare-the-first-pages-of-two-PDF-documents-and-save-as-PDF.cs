using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file paths (first pages will be compared)
        string pdfPath1 = "first.pdf";
        string pdfPath2 = "second.pdf";
        // Output PDF path for side‑by‑side comparison result
        string outputPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"Error: File not found – {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"Error: File not found – {pdfPath2}");
            return;
        }

        try
        {
            // Load the two documents
            Document doc1 = new Document(pdfPath1);
            Document doc2 = new Document(pdfPath2);

            // Ensure each document has at least one page
            if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: One of the PDFs does not contain any pages.");
                return;
            }

            // Retrieve the first page from each document (1‑based indexing)
            Page page1 = doc1.Pages[1];
            Page page2 = doc2.Pages[1];

            // Configure side‑by‑side comparison options (default options are sufficient)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform the comparison; the method saves the result directly to outputPath
            SideBySidePdfComparer.Compare(page1, page2, outputPath, options);

            Console.WriteLine($"Comparison completed successfully. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}