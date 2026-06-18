using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfHelper
{
    public static class PdfHelper
    {
        /// <summary>
        /// Loads a filled PDF from the specified file path and returns its content as a byte array.
        /// The method uses Aspose.Pdf.Facades.PdfViewer to avoid writing the PDF to disk.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF file.</param>
        /// <returns>Byte array containing the PDF data.</returns>
        public static byte[] ConvertPdfToByteArray(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            // PdfViewer implements ISaveableFacade and provides BindPdf and Save(Stream) methods.
            using (PdfViewer viewer = new PdfViewer())
            {
                // Load the PDF into the facade.
                viewer.BindPdf(pdfPath);

                // Save the PDF directly into a memory stream.
                using (MemoryStream ms = new MemoryStream())
                {
                    viewer.Save(ms);
                    // Return the underlying byte array.
                    return ms.ToArray();
                }
            }
        }
    }

    // Dummy entry point to satisfy a console‑application project that requires a Main method.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Intentionally left blank – the library functionality is accessed via PdfHelper.
        }
    }
}