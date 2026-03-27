using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class BookletHelper
{
    // Dummy entry point to satisfy the compiler when building as an executable.
    // In a library project this method can be removed.
    public static void Main() { }

    /// <summary>
    /// Creates a booklet PDF from the supplied stream after deleting a page range and resizing pages.
    /// </summary>
    /// <param name="inputPdfStream">Stream containing the source PDF.</param>
    /// <param name="startPage">First page (1‑based) of the range to delete.</param>
    /// <param name="endPage">Last page (1‑based) of the range to delete.</param>
    /// <param name="outputPageSize">Desired size for every remaining page.</param>
    /// <returns>A stream that contains the booklet PDF.</returns>
    public static Stream CreateBooklet(Stream inputPdfStream, int startPage, int endPage, PageSize outputPageSize)
    {
        // Stream that will hold the PDF after page deletion and resizing
        using var modifiedPdf = new MemoryStream();

        // Load the original PDF from the supplied stream
        using (var pdfDocument = new Document(inputPdfStream))
        {
            // Ensure the requested range is within the document bounds
            if (startPage < 1) startPage = 1;
            if (endPage > pdfDocument.Pages.Count) endPage = pdfDocument.Pages.Count;

            // Delete the specified page range (inclusive) – delete from the end to keep indexes stable
            if (endPage >= startPage)
            {
                for (int p = endPage; p >= startPage; p--)
                {
                    pdfDocument.Pages.Delete(p);
                }
            }

            // Resize every remaining page to the desired output size
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                pdfDocument.Pages[pageIndex].SetPageSize(outputPageSize.Width, outputPageSize.Height);
            }

            // Save the intermediate PDF into the memory stream
            pdfDocument.Save(modifiedPdf);
        }

        // Reset position before passing to the editor
        modifiedPdf.Position = 0;

        // Stream that will receive the final booklet PDF
        var bookletStream = new MemoryStream();

        // Create booklet using PdfFileEditor – use overload without PageSize argument
        var fileEditor = new PdfFileEditor();
        fileEditor.MakeBooklet(modifiedPdf, bookletStream);

        // Prepare the output stream for the caller
        bookletStream.Position = 0;
        return bookletStream;
    }
}
