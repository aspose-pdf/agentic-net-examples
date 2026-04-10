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
            if (inputPdfStream == null)
                throw new ArgumentNullException(nameof(inputPdfStream));

            // Output stream that will hold the booklet PDF.
            MemoryStream outputStream = new MemoryStream();

            // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using.
            PdfFileEditor editor = new PdfFileEditor();

            // Perform the booklet conversion.
            bool succeeded = editor.MakeBooklet(inputPdfStream, outputStream);
            if (!succeeded)
                throw new InvalidOperationException("Failed to create booklet PDF.");

            // Reset the position so the caller can read from the beginning.
            outputStream.Position = 0;
            return outputStream;
        }
    }

    // Dummy entry point to satisfy the console‑application requirement.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Placeholder – no operation needed for the library functionality.
            // Example usage (commented out):
            // using var input = File.OpenRead("input.pdf");
            // var booklet = BookletCreator.CreateBookletPdf(input);
            // File.WriteAllBytes("output.pdf", booklet.ToArray());
        }
    }
}