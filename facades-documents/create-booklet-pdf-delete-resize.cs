using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfBookletHelper
    {
        /// <summary>
        /// Creates a booklet PDF from an input PDF stream.
        /// The method deletes the specified pages, resizes the remaining pages,
        /// and then converts the result into a booklet.
        /// </summary>
        /// <param name="pdfInput">Input PDF stream (must be readable).</param>
        /// <param name="pagesToDelete">Zero‑based page numbers to remove (e.g., new int[] {2,3}).</param>
        /// <param name="newWidth">New width of page contents (default space units).</param>
        /// <param name="newHeight">New height of page contents (default space units).</param>
        /// <returns>A MemoryStream containing the booklet PDF. Caller should dispose it.</returns>
        public static Stream CreateBooklet(Stream pdfInput, int[] pagesToDelete, double newWidth, double newHeight)
        {
            if (pdfInput == null) throw new ArgumentNullException(nameof(pdfInput));
            if (pagesToDelete == null) throw new ArgumentNullException(nameof(pagesToDelete));

            // Ensure the input stream is positioned at the beginning.
            pdfInput.Position = 0;

            // Facade instance – PdfFileEditor provides Delete, ResizeContents and MakeBooklet.
            PdfFileEditor editor = new PdfFileEditor();

            // -----------------------------------------------------------------
            // Step 1: Delete the unwanted pages.
            // -----------------------------------------------------------------
            using (MemoryStream afterDelete = new MemoryStream())
            {
                editor.Delete(pdfInput, pagesToDelete, afterDelete);
                afterDelete.Position = 0; // reset for next operation

                // -----------------------------------------------------------------
                // Step 2: Resize the remaining pages.
                // Pass null for the page array to affect all pages.
                // -----------------------------------------------------------------
                using (MemoryStream afterResize = new MemoryStream())
                {
                    editor.ResizeContents(afterDelete, afterResize, null, newWidth, newHeight);
                    afterResize.Position = 0; // reset for next operation

                    // -----------------------------------------------------------------
                    // Step 3: Convert the resized PDF into a booklet.
                    // -----------------------------------------------------------------
                    MemoryStream bookletStream = new MemoryStream();
                    editor.MakeBooklet(afterResize, bookletStream);
                    bookletStream.Position = 0; // ready for reading by the caller
                    return bookletStream; // caller owns the stream
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // The method does nothing; the library can still be used from other projects.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – placeholder to provide a static Main entry point.
        }
    }
}
