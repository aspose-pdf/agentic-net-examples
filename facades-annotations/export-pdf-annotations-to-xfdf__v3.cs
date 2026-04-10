using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    /// <summary>
    /// Helper class that works with PDF annotations.
    /// </summary>
    public static class PdfAnnotationHelper
    {
        /// <summary>
        /// Binds a PDF from a memory buffer, exports all its annotations to XFDF,
        /// and returns the XFDF data as a byte array.
        /// </summary>
        /// <param name="pdfBytes">Byte array containing the PDF document.</param>
        /// <returns>Byte array containing the exported XFDF.</returns>
        public static byte[] ExportAnnotationsToXfdf(byte[] pdfBytes)
        {
            // Input stream wrapping the PDF byte array
            using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
            // PdfAnnotationEditor is a disposable facade for annotation operations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF document to the editor via the input stream
                editor.BindPdf(pdfStream);

                // Output stream that will receive the XFDF data
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    // Export all annotations to the XFDF stream
                    editor.ExportAnnotationsToXfdf(xfdfStream);

                    // Return the XFDF content as a byte array
                    return xfdfStream.ToArray();
                }
            }
        }
    }

    // ---------------------------------------------------------------------
    // A minimal entry point is required when the project is built as an
    // executable.  Adding a no‑op Main satisfies the compiler without
    // affecting the library functionality.
    // ---------------------------------------------------------------------
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Intentionally left blank – the library is intended to be used
            // programmatically via PdfAnnotationHelper.ExportAnnotationsToXfdf.
        }
    }
}
