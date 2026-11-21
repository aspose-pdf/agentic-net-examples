using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPathA = "documentA.pdf";
        const string pdfPathB = "documentB.pdf";

        if (!File.Exists(pdfPathA) || !File.Exists(pdfPathB))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        // Load both PDFs inside using blocks for deterministic disposal
        using (Document docA = new Document(pdfPathA))
        using (Document docB = new Document(pdfPathB))
        {
            // Verify page count equality
            if (docA.Pages.Count != docB.Pages.Count)
            {
                Console.WriteLine($"Page count mismatch: A={docA.Pages.Count}, B={docB.Pages.Count}");
                // Continue comparison for the minimum number of pages
            }

            int pageCount = Math.Min(docA.Pages.Count, docB.Pages.Count);
            const double sizeTolerance = 0.1; // points tolerance for width/height differences

            for (int i = 1; i <= pageCount; i++) // 1‑based indexing as required by Aspose.Pdf
            {
                Page pageA = docA.Pages[i];
                Page pageB = docB.Pages[i];

                // Compare page dimensions
                double widthDiff  = Math.Abs(pageA.PageInfo.Width  - pageB.PageInfo.Width);
                double heightDiff = Math.Abs(pageA.PageInfo.Height - pageB.PageInfo.Height);

                if (widthDiff > sizeTolerance || heightDiff > sizeTolerance)
                {
                    Console.WriteLine($"Page {i}: Size differs (A: {pageA.PageInfo.Width}x{pageA.PageInfo.Height}, " +
                                      $"B: {pageB.PageInfo.Width}x{pageB.PageInfo.Height})");
                }
                else
                {
                    Console.WriteLine($"Page {i}: Sizes are effectively equal.");
                }

                // Extract text from each page
                TextAbsorber absorberA = new TextAbsorber();
                TextAbsorber absorberB = new TextAbsorber();

                // Use pure text extraction to avoid formatting differences
                absorberA.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                absorberB.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

                pageA.Accept(absorberA);
                pageB.Accept(absorberB);

                string textA = absorberA.Text?.Trim() ?? string.Empty;
                string textB = absorberB.Text?.Trim() ?? string.Empty;

                // Compare extracted text
                if (string.Equals(textA, textB, StringComparison.Ordinal))
                {
                    Console.WriteLine($"Page {i}: Text content matches.");
                }
                else
                {
                    Console.WriteLine($"Page {i}: Text content differs.");
                    // Optionally, show a diff snippet
                    Console.WriteLine($"   A: {Truncate(textA, 100)}");
                    Console.WriteLine($"   B: {Truncate(textB, 100)}");
                }
            }

            Console.WriteLine("Comparison completed.");
        }
    }

    // Helper to truncate long strings for console output
    private static string Truncate(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
    }
}