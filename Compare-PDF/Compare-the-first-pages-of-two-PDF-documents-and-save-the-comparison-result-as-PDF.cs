using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file paths (first and second documents)
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Output PDF file path for the side‑by‑side comparison result
        const string outputPdfPath = "comparison_result.pdf";

        // Verify that both input files exist before proceeding
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

        try
        {
            // Load the two PDF documents
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Ensure both documents contain at least one page
            if (firstDoc.Pages.Count == 0 || secondDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: One of the documents does not contain any pages.");
                return;
            }

            // Retrieve the first page from each document (Aspose.Pdf uses 1‑based indexing)
            Page firstPage = firstDoc.Pages[1];
            Page secondPage = secondDoc.Pages[1];

            // Create default side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform the comparison of the two pages and save the result
            // The Compare method writes the output directly to the specified file.
            SideBySidePdfComparer.Compare(firstPage, secondPage, outputPdfPath, options);

            Console.WriteLine($"Comparison completed successfully. Result saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}