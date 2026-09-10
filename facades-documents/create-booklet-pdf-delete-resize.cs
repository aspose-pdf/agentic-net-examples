using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfProcessor
    {
        /// <summary>
        /// Accepts a PDF stream, deletes the specified pages, resizes all pages,
        /// and returns a booklet version of the resulting document as a stream.
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

            // Intermediate stream after page deletion.
            using var afterDelete = new MemoryStream();

            // Intermediate stream after resizing.
            using var afterResize = new MemoryStream();

            // Final booklet stream to be returned.
            var bookletStream = new MemoryStream();

            // PdfFileEditor provides Delete, ResizeContents, and MakeBooklet operations.
            // NOTE: PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
            var editor = new PdfFileEditor();

            // 1. Delete the unwanted pages.
            // The Delete method writes the result into afterDelete.
            editor.Delete(inputPdf, pagesToDelete, afterDelete);
            afterDelete.Position = 0; // Reset for the next operation.

            // 2. Resize all pages (null pages array means all pages).
            // This shrinks the content to the specified width/height and adds margins.
            editor.ResizeContents(afterDelete, afterResize, null, newWidth, newHeight);
            afterResize.Position = 0; // Reset for the next operation.

            // 3. Create a booklet from the resized document.
            editor.MakeBooklet(afterResize, bookletStream);
            bookletStream.Position = 0; // Prepare the stream for the caller.

            // No need to dispose editor (it has no unmanaged resources).
            return bookletStream;
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Intentionally left blank – the library functionality is exposed via PdfProcessor.
        }
    }
}
