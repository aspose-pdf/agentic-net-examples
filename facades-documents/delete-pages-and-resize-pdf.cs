using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfProcessor
    {
        /// <summary>
        /// Deletes specified pages from a PDF, resizes the remaining pages, and returns the result as a MemoryStream.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF file.</param>
        /// <param name="pagesToDelete">Array of 1‑based page numbers to remove.</param>
        /// <param name="newWidth">New width for page contents (default space units, 1 unit = 1/72 inch).</param>
        /// <param name="newHeight">New height for page contents (default space units).</param>
        /// <returns>A MemoryStream containing the processed PDF. Caller is responsible for disposing the stream.</returns>
        public static MemoryStream DeletePagesAndResize(string pdfPath, int[] pagesToDelete, double newWidth, double newHeight)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("Input PDF file not found.", pdfPath);

            // Stream that will hold the PDF after page deletion
            using var afterDeleteStream = new MemoryStream();

            // Use PdfFileEditor to delete pages
            using (FileStream inputFileStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
            {
                var editor = new PdfFileEditor();
                editor.Delete(inputFileStream, pagesToDelete, afterDeleteStream);
            }

            // Prepare the stream for the next operation
            afterDeleteStream.Position = 0;

            // Stream that will hold the final resized PDF
            var finalStream = new MemoryStream();

            // Resize contents of all remaining pages (null pages array means all pages)
            var resizeEditor = new PdfFileEditor();
            resizeEditor.ResizeContents(afterDeleteStream, finalStream, null, newWidth, newHeight);

            // Reset position so the caller can read from the beginning
            finalStream.Position = 0;

            // Return the resulting stream (caller must dispose)
            return finalStream;
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – the library functionality is exposed via PdfProcessor.
        }
    }
}
