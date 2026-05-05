using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    public static class PdfPageInserter
    {
        /// <summary>
        /// Inserts specified pages from a source PDF into a destination PDF that is loaded from memory.
        /// The resulting PDF is returned as a MemoryStream.
        /// </summary>
        /// <param name="destinationPdf">Byte array containing the destination PDF.</param>
        /// <param name="sourcePdf">Byte array containing the source PDF.</param>
        /// <param name="insertLocation">
        /// 1‑based position in the destination PDF where the pages will be inserted.
        /// For example, 1 inserts before the first page.
        /// </param>
        /// <param name="pageNumbers">
        /// Array of 1‑based page numbers from the source PDF to be inserted.
        /// </param>
        /// <returns>MemoryStream containing the merged PDF.</returns>
        public static MemoryStream InsertPages(
            byte[] destinationPdf,
            byte[] sourcePdf,
            int insertLocation,
            int[] pageNumbers)
        {
            if (destinationPdf == null) throw new ArgumentNullException(nameof(destinationPdf));
            if (sourcePdf == null) throw new ArgumentNullException(nameof(sourcePdf));
            if (pageNumbers == null) throw new ArgumentNullException(nameof(pageNumbers));

            // Output stream will hold the resulting PDF.
            var outputStream = new MemoryStream();

            // Load both PDFs into memory streams.
            using (var destStream = new MemoryStream(destinationPdf))
            using (var srcStream = new MemoryStream(sourcePdf))
            {
                // PdfFileEditor does NOT implement IDisposable; instantiate directly.
                var editor = new PdfFileEditor();

                // Insert the selected pages. Returns true on success.
                bool success = editor.Insert(
                    destStream,          // input PDF (destination)
                    insertLocation,      // where to insert (1‑based)
                    srcStream,           // source PDF
                    pageNumbers,         // pages to insert from source
                    outputStream);       // result PDF

                if (!success)
                    throw new InvalidOperationException("Failed to insert pages using PdfFileEditor.");
            }

            // Reset position so the caller can read from the beginning.
            outputStream.Position = 0;
            return outputStream;
        }
    }

    // Dummy entry point – required when the project is compiled as an executable.
    internal class Program
    {
        private static void Main()
        {
            // Intentionally left blank. The library functionality is accessed via PdfPageInserter.
        }
    }
}