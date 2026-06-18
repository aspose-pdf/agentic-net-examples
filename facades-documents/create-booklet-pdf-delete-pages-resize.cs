using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfProcessor
{
    /// <summary>
    /// Accepts a PDF stream, deletes the specified pages, resizes the page contents,
    /// creates a booklet layout, and returns the resulting PDF as a stream.
    /// </summary>
    /// <param name="inputPdf">Input PDF stream (must be readable and seekable).</param>
    /// <param name="pagesToDelete">Array of 1‑based page numbers to remove.</param>
    /// <param name="newWidth">New width of page contents in default space units (points).</param>
    /// <param name="newHeight">New height of page contents in default space units (points).</param>
    /// <returns>A MemoryStream containing the booklet PDF.</returns>
    public static Stream CreateBooklet(Stream inputPdf, int[] pagesToDelete, double newWidth, double newHeight)
    {
        // Ensure the input stream is positioned at the beginning.
        if (inputPdf.CanSeek)
            inputPdf.Position = 0;

        // PdfFileEditor provides the required facade operations.
        PdfFileEditor editor = new PdfFileEditor();

        // Step 1: Delete the unwanted pages.
        using (MemoryStream afterDelete = new MemoryStream())
        {
            editor.Delete(inputPdf, pagesToDelete, afterDelete);
            afterDelete.Position = 0; // Reset for the next operation.

            // Step 2: Resize the contents of all pages.
            using (MemoryStream afterResize = new MemoryStream())
            {
                // Passing null for the pages array processes all pages.
                editor.ResizeContents(afterDelete, afterResize, null, newWidth, newHeight);
                afterResize.Position = 0; // Reset for the next operation.

                // Step 3: Create a booklet from the resized PDF.
                MemoryStream bookletStream = new MemoryStream();
                editor.MakeBooklet(afterResize, bookletStream);
                bookletStream.Position = 0; // Prepare the stream for the caller.

                return bookletStream; // Caller is responsible for disposing the returned stream.
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main()
    {
        // Intentionally left blank – the library functionality is accessed via PdfProcessor.
    }
}
