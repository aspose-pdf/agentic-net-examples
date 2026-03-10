using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf; // Document class provides page information

public class PdfPageBreakDetector
{
    /// <summary>
    /// Detects page‑break positions in the specified PDF file.
    /// Since a PDF is inherently a sequence of pages, each page start is considered a page‑break.
    /// The method returns the 1‑based page numbers that start a new page.
    /// </summary>
    /// <param name="pdfPath">Full path to the input PDF file.</param>
    /// <returns>List of page numbers where a page break occurs.</returns>
    public static List<int> GetPageBreakPositions(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // Load the PDF document using the high‑level Document class (Aspose.Pdf namespace).
        Document pdfDocument = new Document(pdfPath);

        // The total number of pages in the document.
        int pageCount = pdfDocument.Pages.Count;

        // Build a list of page numbers (1‑based) representing break positions.
        List<int> breakPositions = new List<int>(pageCount);
        for (int i = 1; i <= pageCount; i++)
        {
            breakPositions.Add(i);
        }

        // No unmanaged resources to dispose – Document implements IDisposable, but we can let the GC handle it
        // or wrap it in a using statement if explicit disposal is desired.
        return breakPositions;
    }

    // Example usage
    public static void Main()
    {
        const string inputPdf = "sample.pdf";

        try
        {
            List<int> pageBreaks = GetPageBreakPositions(inputPdf);
            Console.WriteLine($"Detected page‑break positions in '{inputPdf}':");
            foreach (int pageNum in pageBreaks)
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
