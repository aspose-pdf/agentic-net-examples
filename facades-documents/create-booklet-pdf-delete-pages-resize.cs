using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfBookletHelper
{
    /// <summary>
    /// Accepts a PDF stream, deletes the specified pages, resizes all pages,
    /// and returns a booklet PDF as a stream.
    /// </summary>
    /// <param name="inputPdf">Input PDF stream (must be readable and seekable).</param>
    /// <param name="pagesToDelete">Array of page numbers (1‑based) to remove.</param>
    /// <param name="newWidth">New width of page contents in default space units.</param>
    /// <param name="newHeight">New height of page contents in default space units.</param>
    /// <returns>A MemoryStream containing the booklet PDF.</returns>
    public static Stream CreateBooklet(Stream inputPdf, int[] pagesToDelete, double newWidth, double newHeight)
    {
        if (inputPdf == null) throw new ArgumentNullException(nameof(inputPdf));
        if (pagesToDelete == null) throw new ArgumentNullException(nameof(pagesToDelete));

        // Ensure the input stream is positioned at the beginning.
        inputPdf.Position = 0;

        // 1. Delete the unwanted pages.
        using (MemoryStream afterDelete = new MemoryStream())
        {
            PdfFileEditor editor = new PdfFileEditor();
            editor.Delete(inputPdf, pagesToDelete, afterDelete);
            afterDelete.Position = 0;

            // 2. Resize the contents of all remaining pages.
            using (MemoryStream afterResize = new MemoryStream())
            {
                // Passing null for the pages array applies the resize to all pages.
                editor.ResizeContents(afterDelete, afterResize, null, newWidth, newHeight);
                afterResize.Position = 0;

                // 3. Create a booklet from the resized document.
                MemoryStream bookletStream = new MemoryStream();
                editor.MakeBooklet(afterResize, bookletStream);
                bookletStream.Position = 0; // Reset for the caller.

                // Return the booklet stream (caller is responsible for disposing it).
                return bookletStream;
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main()
    {
        // Intentionally left blank – the library functionality is accessed via PdfBookletHelper.
    }
}
