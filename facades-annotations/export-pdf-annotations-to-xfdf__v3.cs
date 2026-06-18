using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfAnnotationHelper
    {
        /// <summary>
        /// Binds a PDF from a memory stream, exports all annotations to XFDF,
        /// and returns the XFDF data as a byte array.
        /// </summary>
        /// <param name="pdfBytes">Byte array containing the source PDF.</param>
        /// <returns>Byte array containing the exported XFDF.</returns>
        public static byte[] ExportAnnotationsToXfdf(byte[] pdfBytes)
        {
            // Validate input
            if (pdfBytes == null || pdfBytes.Length == 0)
                throw new ArgumentException("PDF data must be a non‑empty byte array.", nameof(pdfBytes));

            // Load the PDF into a MemoryStream and bind it to the editor
            using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfStream);

                // Export annotations to an XFDF stream
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    editor.ExportAnnotationsToXfdf(xfdfStream);
                    return xfdfStream.ToArray();
                }
            }
        }
    }

    // ---------------------------------------------------------------------
    // Minimal entry point – required because the project is built as an
    // executable (CS5001). The method does not perform any work; it simply
    // satisfies the compiler. Real usage should be performed from another
    // project or unit test that references this library.
    // ---------------------------------------------------------------------
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Example (commented out) showing how the helper could be used:
            // byte[] pdf = File.ReadAllBytes("sample.pdf");
            // byte[] xfdf = PdfAnnotationHelper.ExportAnnotationsToXfdf(pdf);
            // File.WriteAllBytes("sample.xfdf", xfdf);
        }
    }
}
