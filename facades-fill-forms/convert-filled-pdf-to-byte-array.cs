using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    /// <summary>
    /// Helper methods for converting PDFs to byte arrays without touching the file system.
    /// </summary>
    public static class PdfUtility
    {
        /// <summary>
        /// Loads a filled PDF from a file path and returns its content as a byte array.
        /// Uses Aspose.Pdf.Facades.PdfViewer to avoid writing to disk.
        /// </summary>
        /// <param name="pdfFilePath">Full path to the source PDF file.</param>
        /// <returns>Byte array containing the PDF data.</returns>
        public static byte[] GetPdfBytes(string pdfFilePath)
        {
            if (string.IsNullOrEmpty(pdfFilePath))
                throw new ArgumentException("PDF file path must be provided.", nameof(pdfFilePath));

            if (!File.Exists(pdfFilePath))
                throw new FileNotFoundException("PDF file not found.", pdfFilePath);

            using (PdfViewer viewer = new PdfViewer())
            {
                viewer.BindPdf(pdfFilePath);

                using (MemoryStream outputStream = new MemoryStream())
                {
                    viewer.Save(outputStream);
                    return outputStream.ToArray();
                }
            }
        }

        /// <summary>
        /// Loads a filled PDF from an input stream and returns its content as a byte array.
        /// Useful when the PDF is already in memory (e.g., received from another API).
        /// </summary>
        /// <param name="pdfInputStream">Stream containing the source PDF.</param>
        /// <returns>Byte array containing the PDF data.</returns>
        public static byte[] GetPdfBytes(Stream pdfInputStream)
        {
            if (pdfInputStream == null)
                throw new ArgumentNullException(nameof(pdfInputStream));

            if (pdfInputStream.CanSeek)
                pdfInputStream.Position = 0;

            using (PdfViewer viewer = new PdfViewer())
            {
                viewer.BindPdf(pdfInputStream);

                using (MemoryStream outputStream = new MemoryStream())
                {
                    viewer.Save(outputStream);
                    return outputStream.ToArray();
                }
            }
        }
    }

    // ---------------------------------------------------------------------
    // Minimal entry point – required only because the project is built as
    // an executable. The class library can still be consumed from other
    // projects or unit‑tests.
    // ---------------------------------------------------------------------
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Intentionally left blank.
        }
    }
}
