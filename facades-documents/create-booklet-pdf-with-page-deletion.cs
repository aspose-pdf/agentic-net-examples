using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    public static class PdfBookletHelper
    {
        /// <summary>
        /// Creates a booklet PDF from the input stream.
        /// The method deletes the specified pages, resizes all pages,
        /// and then converts the result into a booklet.
        /// </summary>
        /// <param name="inputPdf">Input PDF stream (must be readable).</param>
        /// <param name="pagesToDelete">Array of page numbers to delete (1‑based). Pass null to keep all pages.</param>
        /// <param name="newWidth">New width of page contents (default space units).</param>
        /// <param name="newHeight">New height of page contents (default space units).</param>
        /// <returns>A MemoryStream containing the booklet PDF. Caller is responsible for disposing it.</returns>
        public static Stream CreateBooklet(Stream inputPdf, int[] pagesToDelete, double newWidth, double newHeight)
        {
            // Ensure the input stream is positioned at the beginning.
            if (inputPdf.CanSeek)
                inputPdf.Position = 0;

            // PdfFileEditor provides the required facade operations.
            PdfFileEditor editor = new PdfFileEditor();

            // -----------------------------------------------------------------
            // Step 1: Delete the unwanted pages.
            // -----------------------------------------------------------------
            using (MemoryStream deleteResult = new MemoryStream())
            {
                // If pagesToDelete is null, use an empty array (no deletion).
                int[] deletePages = pagesToDelete ?? Array.Empty<int>();
                editor.Delete(inputPdf, deletePages, deleteResult);
                deleteResult.Position = 0; // Reset for the next operation.

                // -----------------------------------------------------------------
                // Step 2: Resize the contents of all pages.
                // -----------------------------------------------------------------
                using (MemoryStream resizeResult = new MemoryStream())
                {
                    // Passing null for the pages array applies the resize to all pages.
                    editor.ResizeContents(deleteResult, resizeResult, null, newWidth, newHeight);
                    resizeResult.Position = 0; // Reset for the next operation.

                    // -----------------------------------------------------------------
                    // Step 3: Convert the resized PDF into a booklet.
                    // -----------------------------------------------------------------
                    MemoryStream bookletResult = new MemoryStream();
                    editor.MakeBooklet(resizeResult, bookletResult);
                    bookletResult.Position = 0; // Ensure the caller reads from the start.

                    // Return the booklet stream (caller must dispose it).
                    return bookletResult;
                }
            }
        }
    }

    // ---------------------------------------------------------------------
    // Dummy entry point – required only when the project is built as an
    // executable. The helper class can still be used from other libraries.
    // ---------------------------------------------------------------------
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Example usage (commented out). In production code the helper
            // method would be called from elsewhere.
            // using var input = File.OpenRead("sample.pdf");
            // var booklet = PdfBookletHelper.CreateBooklet(input, new[] { 2, 3 }, 595, 842);
            // using var output = File.Create("booklet.pdf");
            // booklet.CopyTo(output);
        }
    }
}