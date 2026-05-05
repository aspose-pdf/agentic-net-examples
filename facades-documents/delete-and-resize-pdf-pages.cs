using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

public static class PdfProcessor
{
    /// <summary>
    /// Deletes specified pages from a PDF, resizes the remaining pages, and returns the result as a MemoryStream.
    /// </summary>
    /// <param name="inputPdfPath">Full path to the source PDF file.</param>
    /// <param name="pagesToDelete">
    /// 1‑based page numbers to delete (e.g., new int[] { 2, 3 } deletes pages 2 and 3).
    /// </param>
    /// <param name="newWidth">
    /// New width for the page contents (in default space units, 1 unit = 1/72 inch).
    /// </param>
    /// <param name="newHeight">
    /// New height for the page contents (in default space units).
    /// </param>
    /// <returns>A MemoryStream containing the processed PDF. Caller is responsible for disposing it.</returns>
    public static MemoryStream DeleteAndResize(string inputPdfPath, int[] pagesToDelete, double newWidth, double newHeight)
    {
        if (string.IsNullOrEmpty(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException("Input PDF file not found.", inputPdfPath);

        // Load the PDF document.
        Document pdfDocument = new Document(inputPdfPath);

        // Delete pages. Deleting from highest to lowest index prevents re‑indexing issues.
        if (pagesToDelete != null && pagesToDelete.Length > 0)
        {
            foreach (int pageNumber in pagesToDelete.OrderByDescending(p => p))
            {
                if (pageNumber >= 1 && pageNumber <= pdfDocument.Pages.Count)
                    pdfDocument.Pages.Delete(pageNumber);
            }
        }

        // Resize each remaining page.
        foreach (Page page in pdfDocument.Pages)
        {
            page.PageInfo.Width = newWidth;
            page.PageInfo.Height = newHeight;
        }

        // Save the modified document into a MemoryStream.
        MemoryStream resultStream = new MemoryStream();
        pdfDocument.Save(resultStream);
        resultStream.Position = 0; // Reset for reading by the caller.
        return resultStream;
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library functionality is accessed via PdfProcessor.
    }
}