using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string firstPdfPath  = "doc1.pdf";
        const string secondPdfPath = "doc2.pdf";
        // Output file that will contain the side‑by‑side comparison result
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Determine the size of the first page of each document.
            // The footer rectangle is defined as a strip at the bottom of the page.
            // Adjust footerHeight as needed for your specific PDFs.
            const double footerHeight = 50.0;

            Page page1 = doc1.Pages[1]; // 1‑based indexing
            Page page2 = doc2.Pages[1];

            // Rectangle(left, bottom, right, top)
            Aspose.Pdf.Rectangle footerRect1 = new Aspose.Pdf.Rectangle(
                0,                                 // left
                0,                                 // bottom
                page1.PageInfo.Width,              // right (page width)
                footerHeight);                     // top (height of footer area)

            Aspose.Pdf.Rectangle footerRect2 = new Aspose.Pdf.Rectangle(
                0,
                0,
                page2.PageInfo.Width,
                footerHeight);

            // Configure comparison options to exclude the defined footer areas
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                ExcludeAreas1 = new Aspose.Pdf.Rectangle[] { footerRect1 },
                ExcludeAreas2 = new Aspose.Pdf.Rectangle[] { footerRect2 }
                // ExcludeTables defaults to false; no need to set explicitly
            };

            // Perform side‑by‑side comparison; the result is written to resultPdfPath
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}