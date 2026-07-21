using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfPageComparer
{
    /// <summary>
    /// Compares the specified pages of two PDF documents and saves the side‑by‑side comparison result.
    /// </summary>
    /// <param name="firstPdfPath">Path to the first PDF file.</param>
    /// <param name="secondPdfPath">Path to the second PDF file.</param>
    /// <param name="pagesToCompare">Array of 1‑based page numbers that should be compared.</param>
    /// <param name="resultPdfPath">Path where the comparison PDF will be saved.</param>
    public static void CompareSelectedPages(string firstPdfPath, string secondPdfPath, int[] pagesToCompare, string resultPdfPath)
    {
        if (!File.Exists(firstPdfPath))
            throw new FileNotFoundException($"File not found: {firstPdfPath}");
        if (!File.Exists(secondPdfPath))
            throw new FileNotFoundException($"File not found: {secondPdfPath}");
        if (pagesToCompare == null || pagesToCompare.Length == 0)
            throw new ArgumentException("At least one page number must be supplied.", nameof(pagesToCompare));

        // Load the source documents inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create temporary documents that will contain only the pages we want to compare.
            using (Document subset1 = new Document())
            using (Document subset2 = new Document())
            {
                // Copy the requested pages (Aspose.Pdf uses 1‑based indexing).
                foreach (int pageNum in pagesToCompare)
                {
                    if (pageNum < 1 || pageNum > doc1.Pages.Count)
                        throw new ArgumentOutOfRangeException(nameof(pagesToCompare), $"Page {pageNum} is out of range for the first document.");
                    if (pageNum < 1 || pageNum > doc2.Pages.Count)
                        throw new ArgumentOutOfRangeException(nameof(pagesToCompare), $"Page {pageNum} is out of range for the second document.");

                    // Add a copy of the page to each subset document.
                    subset1.Pages.Add(doc1.Pages[pageNum]);
                    subset2.Pages.Add(doc2.Pages[pageNum]);
                }

                // Prepare comparison options (default options are sufficient for a basic side‑by‑side view).
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Perform the comparison and save the result.
                SideBySidePdfComparer.Compare(subset1, subset2, resultPdfPath, options);
            }
        }
    }

    // Example usage.
    static void Main()
    {
        string pdfA = "DocumentA.pdf";
        string pdfB = "DocumentB.pdf";
        int[] pages = { 2, 4, 7 };               // pages to compare (1‑based)
        string output = "ComparisonResult.pdf";

        try
        {
            CompareSelectedPages(pdfA, pdfB, pages, output);
            Console.WriteLine($"Comparison saved to '{output}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}