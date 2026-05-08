using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfProcessor
{
    /// <summary>
    /// Accepts a PDF stream, deletes the specified pages, resizes the remaining pages,
    /// and returns a booklet PDF stream.
    /// </summary>
    /// <param name="inputPdf">Input PDF stream (must be readable).</param>
    /// <param name="pagesToDelete">Array of page numbers (1‑based) to remove.</param>
    /// <param name="newWidth">New width of page contents (default space units).</param>
    /// <param name="newHeight">New height of page contents (default space units).</param>
    /// <returns>A MemoryStream containing the booklet PDF.</returns>
    public static MemoryStream CreateBooklet(Stream inputPdf, int[] pagesToDelete, double newWidth, double newHeight)
    {
        // Ensure the input stream is at the beginning.
        inputPdf.Position = 0;

        // -----------------------------------------------------------------
        // Step 1: Delete the unwanted pages.
        // -----------------------------------------------------------------
        MemoryStream afterDelete = new MemoryStream();
        // PdfFileEditor does NOT implement IDisposable – instantiate directly.
        var deleteEditor = new PdfFileEditor();
        deleteEditor.Delete(inputPdf, pagesToDelete, afterDelete);
        afterDelete.Position = 0; // Reset for next operation.

        // -----------------------------------------------------------------
        // Step 2: Resize the contents of the remaining pages.
        // -----------------------------------------------------------------
        MemoryStream afterResize = new MemoryStream();
        var resizeEditor = new PdfFileEditor();
        // Passing null for the pages array means "all pages".
        resizeEditor.ResizeContents(afterDelete, afterResize, null, newWidth, newHeight);
        afterResize.Position = 0; // Reset for next operation.

        // -----------------------------------------------------------------
        // Step 3: Convert the resized PDF into a booklet.
        // -----------------------------------------------------------------
        MemoryStream bookletStream = new MemoryStream();
        var bookletEditor = new PdfFileEditor();
        bookletEditor.MakeBooklet(afterResize, bookletStream);
        bookletStream.Position = 0; // Ready for the caller to read.

        // Return the final booklet stream.
        return bookletStream;
    }
}

// A minimal entry point is required because the project is built as a console
// application. The method is intentionally left empty – the library functionality
// is exposed via PdfProcessor.CreateBooklet.
public class Program
{
    public static void Main()
    {
        // No‑op entry point.
    }
}