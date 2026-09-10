using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison; // Comparison API

class Program
{
    static void Main()
    {
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(doc1Path))
        using (Document doc2 = new Document(doc2Path))
        {
            // Assume both documents have the same page size; use the first page to define the footer area
            Page firstPage = doc1.Pages[1];
            double pageWidth = firstPage.PageInfo.Width;
            double footerHeight = 50; // Height of the footer region to exclude (adjust as needed)

            // Define a rectangle that covers the footer area (bottom of the page)
            Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(0, 0, pageWidth, footerHeight);

            // Configure comparison options with exclude areas for both documents
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                ExcludeAreas1 = new Aspose.Pdf.Rectangle[] { footerRect },
                ExcludeAreas2 = new Aspose.Pdf.Rectangle[] { footerRect }
            };

            // Perform side‑by‑side comparison and save the result PDF
            SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }
}