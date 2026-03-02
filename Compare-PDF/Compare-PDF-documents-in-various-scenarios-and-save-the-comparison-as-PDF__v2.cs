using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonDemo
{
    static void Main()
    {
        // Input PDF files
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";

        // Output files for the different comparison scenarios
        const string sideBySideResult = "comparison_side_by_side.pdf";
        const string pageResult = "comparison_pages.pdf";
        const string textResult = "comparison_text.pdf";

        // Verify that the source files exist
        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // ------------------------------------------------------------
        // 1. Whole‑document side‑by‑side visual comparison
        // ------------------------------------------------------------
        using (Document doc1 = new Document(doc1Path))
        using (Document doc2 = new Document(doc2Path))
        {
            // Configure side‑by‑side options (optional)
            var sideBySideOptions = new SideBySideComparisonOptions
            {
                // Show change marks that appear on other pages as well
                AdditionalChangeMarks = true
            };

            // Perform the comparison and write the result PDF
            SideBySidePdfComparer.Compare(doc1, doc2, sideBySideResult, sideBySideOptions);
        }

        Console.WriteLine($"Document side‑by‑side comparison saved to '{sideBySideResult}'.");

        // ------------------------------------------------------------
        // 2. Single‑page side‑by‑side visual comparison
        // ------------------------------------------------------------
        using (Document doc1 = new Document(doc1Path))
        using (Document doc2 = new Document(doc2Path))
        {
            // Pages are 1‑based in Aspose.Pdf
            Page page1 = doc1.Pages[1];
            Page page2 = doc2.Pages[1];

            var pageOptions = new SideBySideComparisonOptions();

            SideBySidePdfComparer.Compare(page1, page2, pageResult, pageOptions);
        }

        Console.WriteLine($"First‑page side‑by‑side comparison saved to '{pageResult}'.");

        // ------------------------------------------------------------
        // 3. Text‑based comparison with custom options
        // ------------------------------------------------------------
        using (Document doc1 = new Document(doc1Path))
        using (Document doc2 = new Document(doc2Path))
        {
            var textOptions = new ComparisonOptions
            {
                // Example: ignore tables when comparing text
                ExcludeTables = true
            };

            // This overload writes the comparison result to a PDF file
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, textOptions, textResult);
        }

        Console.WriteLine($"Text comparison saved to '{textResult}'.");
    }
}