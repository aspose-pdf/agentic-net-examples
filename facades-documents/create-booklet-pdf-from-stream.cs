using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class BookletCreator
    {
        /// <summary>
        /// Creates a booklet PDF from the provided input PDF stream.
        /// The result is returned as a MemoryStream.
        /// </summary>
        /// <param name="inputPdfStream">Stream containing the source PDF.</param>
        /// <returns>MemoryStream with the booklet PDF.</returns>
        public static MemoryStream CreateBookletPdf(Stream inputPdfStream)
        {
            // Ensure the input stream is positioned at the beginning.
            if (inputPdfStream.CanSeek)
                inputPdfStream.Position = 0;

            // Output stream that will hold the booklet PDF.
            MemoryStream outputStream = new MemoryStream();

            // PdfFileEditor does NOT implement IDisposable; instantiate directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Use the MakeBooklet overload that works with streams.
            editor.MakeBooklet(inputPdfStream, outputStream);

            // Reset the output stream position so callers can read from the start.
            if (outputStream.CanSeek)
                outputStream.Position = 0;

            return outputStream;
        }
    }

    // Dummy entry point to satisfy the console‑app requirement of the project.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – placeholder for entry point.
        }
    }
}