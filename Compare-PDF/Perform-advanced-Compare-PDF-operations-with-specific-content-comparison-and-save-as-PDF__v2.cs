using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string firstPdfPath  = "FirstDocument.pdf";
        const string secondPdfPath = "SecondDocument.pdf";

        // Output PDF that will contain the comparison result
        const string resultPdfPath = "ComparisonResult.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks to guarantee disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // ------------------------------------------------------------
            // 1) Side‑by‑side visual comparison (pages are interleaved)
            // ------------------------------------------------------------
            // Create default options for side‑by‑side comparison.
            // Additional options (e.g., colors, page margins) can be set here
            // if needed; the default constructor provides sensible defaults.
            SideBySideComparisonOptions sideBySideOptions = new SideBySideComparisonOptions();

            // Perform the comparison and save the result PDF.
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, sideBySideOptions);

            // ------------------------------------------------------------
            // 2) Text‑based content comparison with custom options
            // ------------------------------------------------------------
            // Configure comparison options – for example, exclude tables from
            // the comparison and define an extraction area if required.
            ComparisonOptions textOptions = new ComparisonOptions
            {
                ExcludeTables = true               // Do not consider table differences
                // ExtractionArea = new Aspose.Pdf.Rectangle(0, 0, 500, 800) // optional
            };

            // The overload that includes a result path saves a PDF that highlights
            // the textual differences. It also returns the diff data structure.
            // The returned value can be inspected or further processed if needed.
            var diffData = TextPdfComparer.CompareDocumentsPageByPage(
                doc1,
                doc2,
                textOptions,
                resultPdfPath); // same output file; it will be overwritten with the last operation

            // (Optional) Process diffData here – e.g., log the number of changes per page.
            // foreach (var pageDiffs in diffData)
            // {
            //     Console.WriteLine($"Page has {pageDiffs.Count} changes.");
            // }
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}