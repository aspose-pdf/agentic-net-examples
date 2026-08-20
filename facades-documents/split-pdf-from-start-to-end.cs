using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    public static class PdfSplitter
    {
        /// <summary>
        /// Splits the PDF contained in <paramref name="inputPdfStream"/> starting from <paramref name="startPage"/>
        /// to the last page and returns the resulting PDF as a <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="inputPdfStream">Stream that holds the source PDF. The stream must be readable.</param>
        /// <param name="startPage">1‑based page number from which the split should begin.</param>
        /// <returns>A MemoryStream containing the split PDF. The caller is responsible for disposing it.</returns>
        public static MemoryStream SplitFromStartToEnd(Stream inputPdfStream, int startPage)
        {
            if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));
            if (startPage < 1) throw new ArgumentOutOfRangeException(nameof(startPage), "Page numbers are 1‑based.");

            // PdfFileEditor does NOT implement IDisposable, so we do NOT wrap it in a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // Output will be written to a MemoryStream. The editor does NOT close the stream,
            // so we can safely return it after the operation.
            MemoryStream outputStream = new MemoryStream();

            // SplitToEnd extracts the rear part of the document starting at the specified page.
            // The method returns true on success; we can optionally check the result.
            bool success = editor.SplitToEnd(inputPdfStream, startPage, outputStream);
            if (!success)
            {
                // If the operation failed, dispose the created stream and throw.
                outputStream.Dispose();
                throw new InvalidOperationException("Failed to split the PDF document.");
            }

            // Reset the position to the beginning so that callers can read from the start.
            outputStream.Position = 0;
            return outputStream;
        }
    }

    // ---------------------------------------------------------------------
    // A minimal entry point is required because the project is built as an
    // executable (Console Application).  The method above is a library‑style
    // helper, so the Main method simply demonstrates a no‑op usage to satisfy
    // the compiler.
    // ---------------------------------------------------------------------
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Example placeholder – real usage would pass a PDF stream and a start page.
            // This stub exists solely to provide a valid entry point for the build.
            Console.WriteLine("PdfSplitter library loaded. No operation performed.");
        }
    }
}
