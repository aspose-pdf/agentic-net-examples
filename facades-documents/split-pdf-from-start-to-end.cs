using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
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
        /// Pages before this number are discarded; the returned stream contains
        /// pages startPage … last page of the source document.
        /// </param>
        /// <returns>A MemoryStream holding the split PDF. The stream's position is set to 0.</returns>
        public static MemoryStream SplitFromStartToEnd(Stream inputPdf, int startPage)
        {
            if (inputPdf == null) throw new ArgumentNullException(nameof(inputPdf));
            if (startPage < 1) throw new ArgumentOutOfRangeException(nameof(startPage), "Page numbers are 1‑based.");

            // Output stream that will receive the rear part of the document.
            var outputPdf = new MemoryStream();

            // PdfFileEditor provides the SplitToEnd method which works with streams.
            var editor = new PdfFileEditor();

            // Perform the split. Returns true on success, false otherwise.
            bool succeeded = editor.SplitToEnd(inputPdf, startPage, outputPdf);
            if (!succeeded)
            {
                // Clean up the output stream before throwing.
                outputPdf.Dispose();
                throw new InvalidOperationException("Failed to split the PDF document.");
            }

            // Reset the position so the caller can read from the beginning.
            outputPdf.Position = 0;
            return outputPdf;
        }
    }

    // Dummy entry point so the project builds as a console application.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Example usage (commented out – replace with real streams when needed):
            // using var input = File.OpenRead("sample.pdf");
            // var result = PdfSplitter.SplitFromStartToEnd(input, 2);
            // File.WriteAllBytes("output.pdf", result.ToArray());
        }
    }
}