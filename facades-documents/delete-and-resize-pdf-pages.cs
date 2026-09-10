using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    public static class PdfProcessor
    {
        /// <summary>
        /// Deletes specified pages from a PDF, resizes the remaining pages, and returns the result as a MemoryStream.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF file.</param>
        /// <param name="pagesToDelete">Array of 1‑based page numbers to remove.</param>
        /// <param name="newWidth">New width for page contents (default units, 1 unit = 1/72 inch).</param>
        /// <param name="newHeight">New height for page contents (default units, 1 unit = 1/72 inch).</param>
        /// <returns>A MemoryStream containing the processed PDF. Caller is responsible for disposing it.</returns>
        public static MemoryStream DeleteAndResizePdf(string pdfPath, int[] pagesToDelete, double newWidth, double newHeight)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("Input PDF file not found.", pdfPath);

            // Open the source PDF as a read‑only stream.
            using (FileStream inputStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
            {
                // Intermediate stream that will hold the PDF after page deletion.
                using (MemoryStream afterDeleteStream = new MemoryStream())
                {
                    PdfFileEditor editor = new PdfFileEditor();

                    // Delete the specified pages. Returns true on success.
                    bool deleteSuccess = editor.Delete(inputStream, pagesToDelete, afterDeleteStream);
                    if (!deleteSuccess)
                        throw new InvalidOperationException("Failed to delete pages from the PDF.");

                    // Prepare the stream for reading by resetting its position.
                    afterDeleteStream.Position = 0;

                    // Final output stream that will contain the resized PDF.
                    MemoryStream resultStream = new MemoryStream();

                    // Resize the contents of all remaining pages.
                    // Passing null for the pages array applies the operation to every page.
                    bool resizeSuccess = editor.ResizeContents(afterDeleteStream, resultStream, null, newWidth, newHeight);
                    if (!resizeSuccess)
                        throw new InvalidOperationException("Failed to resize PDF contents.");

                    // Reset the position so the caller can read from the beginning.
                    resultStream.Position = 0;
                    return resultStream;
                }
            }
        }
    }

    // Dummy entry point – required when the project is built as a console application.
    internal class Program
    {
        static void Main(string[] args)
        {
            // Intentionally left blank. The library functionality is exposed via PdfProcessor.
        }
    }
}
