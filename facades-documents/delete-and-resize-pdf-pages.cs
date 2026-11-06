using System;
using System.IO;
using Aspose.Pdf;

public static class PdfProcessor
{
    /// <summary>
    /// Loads a PDF, deletes the specified pages, resizes all remaining pages to the given size,
    /// and returns the resulting PDF as a stream.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <param name="pagesToDelete">Array of 1‑based page numbers to remove. Pass null or empty array to skip deletion.</param>
    /// <param name="newSize">Desired page size (e.g., PageSize.A4 or new PageSize(width, height)).</param>
    /// <returns>A MemoryStream containing the processed PDF. Caller is responsible for disposing the stream.</returns>
    public static Stream DeleteAndResizePdf(string pdfPath, int[] pagesToDelete, PageSize newSize)
    {
        // Load the PDF directly with Document (recommended over PdfFileEditor for most operations).
        Document pdfDoc = new Document(pdfPath);

        // ---------------------------------------------------------------------
        // Step 1 – Delete pages (if any).
        // ---------------------------------------------------------------------
        if (pagesToDelete != null && pagesToDelete.Length > 0)
        {
            // Aspose.Pdf.Document.Pages.Delete accepts a collection of 1‑based page numbers.
            pdfDoc.Pages.Delete(pagesToDelete);
        }

        // ---------------------------------------------------------------------
        // Step 2 – Resize every remaining page to the requested size.
        // ---------------------------------------------------------------------
        foreach (Page page in pdfDoc.Pages)
        {
            // The SetPageSize overload expects width and height as doubles.
            // Use the Width and Height properties of the PageSize instance.
            page.SetPageSize((double)newSize.Width, (double)newSize.Height);
        }

        // ---------------------------------------------------------------------
        // Step 3 – Save the modified document into a MemoryStream and return it.
        // ---------------------------------------------------------------------
        MemoryStream resultStream = new MemoryStream();
        pdfDoc.Save(resultStream);
        resultStream.Position = 0; // rewind for the caller
        return resultStream;
    }

    // ---------------------------------------------------------------------
    // Dummy entry point – required only to satisfy the compiler when the project
    // is built as an executable. In a real library this method would be removed.
    // ---------------------------------------------------------------------
    public static void Main(string[] args)
    {
        // No operation – the class is intended to be used as a library.
    }
}
