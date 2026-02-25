using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files to compare
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Output PDF that will contain the comparison result
        const string resultPdfPath = "comparison_result.pdf";

        // Validate input files
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
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // ------------------------------------------------------------
                // Document-level graphical comparison
                // The result PDF will contain visual differences between the two docs
                // ------------------------------------------------------------
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);

                // ------------------------------------------------------------
                // Optional: page-by-page graphical comparison
                // Demonstrates comparing the first pages of each document and
                // appending the result to the same output PDF.
                // ------------------------------------------------------------
                if (doc1.Pages.Count > 0 && doc2.Pages.Count > 0)
                {
                    // Retrieve the first pages (1‑based indexing)
                    Page page1 = doc1.Pages[1];
                    Page page2 = doc2.Pages[1];

                    // Create a temporary PDF to hold the page comparison result
                    const string tempPageResult = "temp_page_comparison.pdf";

                    // Compare the two pages and store the result in a separate PDF
                    comparer.ComparePagesToPdf(page1, page2, tempPageResult);

                    // Append the temporary result to the main result PDF
                    using (Document tempDoc = new Document(tempPageResult))
                    {
                        // Open the main result PDF for appending
                        using (Document resultDoc = new Document(resultPdfPath))
                        {
                            resultDoc.Pages.Add(tempDoc.Pages);
                            resultDoc.Save(resultPdfPath);
                        }
                    }

                    // Clean up the temporary file
                    File.Delete(tempPageResult);
                }

                Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
            }
        }
        catch (ArgumentException ex)
        {
            // Thrown if pages have different sizes or invalid arguments are supplied
            Console.Error.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}