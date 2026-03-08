using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

public static class PdfPageBreakDetector
{
    /// <summary>
    /// Detects page‑break positions in the specified PDF.
    /// In a PDF each page represents a break, so the method returns
    /// a list of page numbers (1‑based) that can be used for further processing.
    /// </summary>
    /// <param name="pdfPath">Full path to the input PDF file.</param>
    /// <returns>List of page numbers where page breaks occur.</returns>
    public static List<int> GetPageBreakPositions(string pdfPath)
    {
        // Validate input
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        // Use PdfPageEditor from Aspose.Pdf.Facades to bind the document
        // and obtain the total page count. PdfPageEditor does not implement IDisposable,
        // so no using block is required.
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(pdfPath);

        // Get the number of pages (1‑based indexing)
        int pageCount = editor.GetPages();

        // Build a list of page numbers representing page‑break positions
        List<int> pageBreaks = new List<int>(pageCount);
        for (int i = 1; i <= pageCount; i++)
        {
            pageBreaks.Add(i);
        }

        // No explicit close needed for PdfPageEditor
        return pageBreaks;
    }

    // Example usage
    public static void Main()
    {
        const string inputPdf = "sample.pdf";

        try
        {
            List<int> breaks = GetPageBreakPositions(inputPdf);
            Console.WriteLine($"Detected page‑breaks in '{inputPdf}':");
            foreach (int pageNum in breaks)
            {
                Console.WriteLine($"  Page {pageNum}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}