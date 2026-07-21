using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the PDFs to compare and the output file
        const string pdfPath1 = "input1.pdf";
        const string pdfPath2 = "input2.pdf";
        const string resultPdf = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure comparison options.
            // By default, AcroForm field values are taken into account, so no extra flag is required.
            ComparisonOptions compOptions = new ComparisonOptions
            {
                // Example: keep tables in the comparison (default is false, but set explicitly)
                ExcludeTables = false
            };

            // Optional: configure side‑by‑side visual comparison options
            SideBySideComparisonOptions sbOptions = new SideBySideComparisonOptions
            {
                // Example: include additional change marks if desired
                AdditionalChangeMarks = false,
                ComparisonMode = ComparisonMode.Normal
            };

            // Perform side‑by‑side comparison and save the visual result
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdf, sbOptions);

            // If you need the raw diff operations per page, use TextPdfComparer
            var pageDiffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, compOptions);
            Console.WriteLine($"Differences detected on {pageDiffs.Count} page(s).");
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdf}'.");
    }
}