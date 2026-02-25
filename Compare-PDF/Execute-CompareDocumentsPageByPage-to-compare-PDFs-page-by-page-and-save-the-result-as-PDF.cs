using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPath = "first.pdf";
        const string secondPath = "second.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(firstPath) || !File.Exists(secondPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal.
            using (Document doc1 = new Document(firstPath))
            using (Document doc2 = new Document(secondPath))
            {
                // Configure comparison options (defaults are fine; customize if needed).
                ComparisonOptions options = new ComparisonOptions
                {
                    // Example: ignore spaces during comparison.
                    // ComparisonMode = ComparisonMode.IgnoreSpaces
                };

                // Perform page‑by‑page comparison and save the resulting PDF.
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}