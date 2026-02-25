using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonDemo
{
    static void Main()
    {
        // Input PDF files
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";

        // Output files
        const string wholeDocResult = "whole_document_comparison.pdf";
        const string pageResult = "page_comparison.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // ------------------------------------------------------------
            // 1) Compare the entire documents page‑by‑page (textual comparison)
            // ------------------------------------------------------------
            // Create default comparison options (can be customized if needed)
            ComparisonOptions textOptions = new ComparisonOptions();

            // The static method returns a list of changes per page and also
            // writes the visual comparison result to the specified PDF file.
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, textOptions, wholeDocResult);
            Console.WriteLine($"Whole‑document comparison saved to '{wholeDocResult}'.");

            // ------------------------------------------------------------
            // 2) Compare specific pages (e.g., page 2 of each document) side‑by‑side
            // ------------------------------------------------------------
            // Ensure the documents have at least two pages
            if (doc1.Pages.Count < 2 || doc2.Pages.Count < 2)
            {
                Console.Error.WriteLine("Both documents must contain at least two pages for this example.");
                return;
            }

            // Retrieve the pages (Aspose.Pdf uses 1‑based indexing)
            Page pageFromDoc1 = doc1.Pages[2];
            Page pageFromDoc2 = doc2.Pages[2];

            // Create side‑by‑side comparison options (default works fine)
            SideBySideComparisonOptions sideOptions = new SideBySideComparisonOptions();

            // This method writes the side‑by‑side visual diff directly to a PDF file
            SideBySidePdfComparer.Compare(pageFromDoc1, pageFromDoc2, pageResult, sideOptions);
            Console.WriteLine($"Page‑by‑page side‑by‑side comparison saved to '{pageResult}'.");
        }
    }
}