using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    public static class PdfProcessor
    {
        /// <summary>
        /// Deletes specified pages from a PDF, resizes the remaining pages, and returns the result as a stream.
        /// </summary>
        /// <param name="inputPath">Path to the source PDF file.</param>
        /// <param name="pagesToDelete">Array of 1‑based page numbers to remove.</param>
        /// <param name="newWidth">New width for page contents (default space units).</param>
        /// <param name="newHeight">New height for page contents (default space units).</param>
        /// <returns>A MemoryStream containing the processed PDF.</returns>
        public static MemoryStream ProcessPdf(string inputPath, int[] pagesToDelete, double newWidth, double newHeight)
        {
            // Validate input
            if (string.IsNullOrEmpty(inputPath))
                throw new ArgumentException("Input path must be provided.", nameof(inputPath));
            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input PDF not found.", inputPath);
            if (pagesToDelete == null || pagesToDelete.Length == 0)
                throw new ArgumentException("At least one page number must be specified for deletion.", nameof(pagesToDelete));

            // Stream that will hold the PDF after page deletion
            using var afterDeleteStream = new MemoryStream();
            using (var inputFileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                var fileEditor = new PdfFileEditor();
                // Delete pages and write result to afterDeleteStream
                fileEditor.Delete(inputFileStream, pagesToDelete, afterDeleteStream);
            }

            // Reset position to the beginning before the next operation
            afterDeleteStream.Position = 0;

            // Stream that will hold the final resized PDF
            var finalStream = new MemoryStream();

            // Resize the contents of all pages (pages parameter = null means all pages)
            var resizeEditor = new PdfFileEditor();
            resizeEditor.ResizeContents(afterDeleteStream, finalStream, null, newWidth, newHeight);

            // Reset position so the caller can read from the beginning
            finalStream.Position = 0;

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