using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfSplitDemo
{
    public static class PdfSplitter
    {
        /// <summary>
        /// Splits the input PDF stream from the specified start page to the end
        /// and returns the resulting PDF as a MemoryStream.
        /// </summary>
        /// <param name="inputPdf">Stream containing the source PDF. Must be readable.</param>
        /// <param name="startPage">
        /// 1‑based page number where the split should begin.
        /// Pages before this number are discarded.
        /// </param>
        /// <returns>
        /// MemoryStream holding the split PDF. The stream position is set to 0.
        /// </returns>
        public static MemoryStream SplitFromStartToEnd(Stream inputPdf, int startPage)
        {
            if (inputPdf == null) throw new ArgumentNullException(nameof(inputPdf));
            if (startPage < 1) throw new ArgumentOutOfRangeException(nameof(startPage), "Page numbers are 1‑based.");

            // PdfFileEditor does NOT implement IDisposable, so we do not wrap it in a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // Output will be written to a MemoryStream which we return to the caller.
            MemoryStream outputStream = new MemoryStream();

            // SplitToEnd extracts the rear part of the document starting at startPage.
            // The method does NOT close the streams, so we keep outputStream alive.
            editor.SplitToEnd(inputPdf, startPage, outputStream);

            // Reset the position so the caller can read from the beginning.
            outputStream.Position = 0;
            return outputStream;
        }
    }

    // Minimal entry point required for a console‑application project.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Optional demonstration (can be removed for pure library usage).
            // using var input = File.OpenRead("sample.pdf");
            // using var result = PdfSplitter.SplitFromStartToEnd(input, 3);
            // File.WriteAllBytes("output.pdf", result.ToArray());
        }
    }
}
