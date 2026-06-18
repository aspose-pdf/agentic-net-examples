using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfSplitter
    {
        /// <summary>
        /// Splits the provided PDF stream from the specified start page to the end of the document.
        /// The resulting PDF is returned as a MemoryStream.
        /// </summary>
        /// <param name="inputPdfStream">Input PDF stream (must be readable).</param>
        /// <param name="startPage">1‑based page number where the split should begin.</param>
        /// <returns>A MemoryStream containing the split PDF (front part from startPage to the last page).</returns>
        public static MemoryStream SplitFromStartToEnd(Stream inputPdfStream, int startPage)
        {
            if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));
            if (startPage < 1) throw new ArgumentOutOfRangeException(nameof(startPage), "Page numbers are 1‑based.");

            // Output stream that will hold the split PDF.
            MemoryStream outputStream = new MemoryStream();

            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
            PdfFileEditor editor = new PdfFileEditor();

            // SplitToEnd splits from the given location (inclusive) to the end of the document.
            // The method returns true on success; we let any failure propagate as an exception.
            bool success = editor.SplitToEnd(inputPdfStream, startPage, outputStream);
            if (!success)
            {
                // If the operation failed, clean up and throw an informative exception.
                outputStream.Dispose();
                throw new InvalidOperationException("Failed to split the PDF using PdfFileEditor.SplitToEnd.");
            }

            // Reset the position of the output stream so callers can read from the beginning.
            outputStream.Position = 0;
            return outputStream;
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – the library functionality is exposed via PdfSplitter.
        }
    }
}
