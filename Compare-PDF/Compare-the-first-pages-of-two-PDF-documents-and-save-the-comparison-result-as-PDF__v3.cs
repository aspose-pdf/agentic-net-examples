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
        const string resultPdfPath = "comparison_result.pdf";

        // Verify input files exist
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
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Ensure both documents have at least one page
                if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
                {
                    Console.Error.WriteLine("One of the documents does not contain any pages.");
                    return;
                }

                // Retrieve the first pages (1‑based indexing)
                Page page1 = doc1.Pages[1];
                Page page2 = doc2.Pages[1];

                // Perform graphical comparison of the first pages and save the result as PDF
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.ComparePagesToPdf(page1, page2, resultPdfPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
        }
        catch (ArgumentException ex)
        {
            // Thrown if pages have different sizes or result path is invalid
            Console.Error.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}