using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonDemo
{
    // Compare two entire PDF documents page‑by‑page (text comparison) and save the result as PDF.
    static void CompareWholeDocuments(string docPath1, string docPath2, string resultPdfPath)
    {
        // Load both documents inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(docPath1))
        using (Document doc2 = new Document(docPath2))
        {
            // Create default comparison options (can be customized if needed).
            ComparisonOptions options = new ComparisonOptions();

            // Perform the comparison and save the visual diff PDF.
            // This overload writes the result directly to the specified file.
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);
        }
    }

    // Compare two specific pages (side‑by‑side visual comparison) and save the result as PDF.
    static void CompareSpecificPages(string docPath1, int pageNumber1,
                                     string docPath2, int pageNumber2,
                                     string resultPdfPath)
    {
        // Ensure page numbers are 1‑based (Aspose.Pdf uses 1‑based indexing).
        if (pageNumber1 < 1 || pageNumber2 < 1)
            throw new ArgumentOutOfRangeException("Page numbers must be >= 1.");

        using (Document doc1 = new Document(docPath1))
        using (Document doc2 = new Document(docPath2))
        {
            // Retrieve the requested pages.
            Page page1 = doc1.Pages[pageNumber1];
            Page page2 = doc2.Pages[pageNumber2];

            // Create side‑by‑side comparison options (default settings are fine for most cases).
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform the side‑by‑side comparison and save the result PDF.
            SideBySidePdfComparer.Compare(page1, page2, resultPdfPath, options);
        }
    }

    static void Main()
    {
        // Example file paths (adjust as needed).
        const string firstPdf  = "FirstDocument.pdf";
        const string secondPdf = "SecondDocument.pdf";
        const string wholeResultPdf = "WholeDocumentComparison.pdf";
        const string pageResultPdf = "PageComparison.pdf";

        // Verify input files exist.
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Compare the entire documents.
            CompareWholeDocuments(firstPdf, secondPdf, wholeResultPdf);
            Console.WriteLine($"Whole‑document comparison saved to '{wholeResultPdf}'.");

            // Compare specific pages (e.g., page 2 of first PDF vs page 3 of second PDF).
            CompareSpecificPages(firstPdf, 2, secondPdf, 3, pageResultPdf);
            Console.WriteLine($"Page‑by‑page comparison saved to '{pageResultPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}