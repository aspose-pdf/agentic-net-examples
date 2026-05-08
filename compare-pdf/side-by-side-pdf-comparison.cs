using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the source PDFs and the output comparison PDF
        const string pdfPath1 = "doc1.pdf";
        const string pdfPath2 = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Configure side‑by‑side comparison options
                SideBySideComparisonOptions options = new SideBySideComparisonOptions
                {
                    // Show change markers that appear on other pages as well
                    AdditionalChangeMarks = true
                    // Other options can be set here as needed
                };

                // Perform the comparison; the result is saved to resultPath
                SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
                Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");

                // Verify that the resulting PDF contains the expected number of pages
                using (Document resultDoc = new Document(resultPath))
                {
                    // Side‑by‑side comparison adds pages from both documents sequentially
                    int expectedPages = doc1.Pages.Count + doc2.Pages.Count;
                    if (resultDoc.Pages.Count == expectedPages)
                    {
                        Console.WriteLine($"Verification passed: result contains {expectedPages} pages as expected.");
                    }
                    else
                    {
                        Console.WriteLine($"Verification warning: expected {expectedPages} pages but found {resultDoc.Pages.Count}.");
                    }
                }
            }
        }
        catch (ArgumentException ex)
        {
            // Handles cases where a comparer that requires identical page sizes is mistakenly used
            Console.Error.WriteLine($"Argument error during comparison: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}