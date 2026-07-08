using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    /// <summary>
    /// Provides helper methods for working with PDF annotations.
    /// </summary>
    public static class PdfAnnotationHelper
    {
        /// <summary>
        /// Binds a PDF from a memory buffer, exports all its annotations to XFDF,
        /// and returns the XFDF data as a byte array.
        /// </summary>
        /// <param name="pdfData">Byte array containing the source PDF.</param>
        /// <returns>Byte array with the exported XFDF content.</returns>
        public static byte[] ExportAnnotationsToXfdf(byte[] pdfData)
        {
            // Input stream containing the PDF document
            using (MemoryStream pdfStream = new MemoryStream(pdfData))
            // Facade for working with PDF annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF document to the editor (no need for a file path)
                editor.BindPdf(pdfStream);

                // Output stream that will receive the XFDF data
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    // Export all annotations to the XFDF stream
                    editor.ExportAnnotationsToXfdf(xfdfStream);

                    // Return the XFDF bytes; ToArray works regardless of the stream position
                    return xfdfStream.ToArray();
                }
            }
        }
    }

    // Dummy entry point to satisfy a console‑type project configuration.
    internal class Program
    {
        static void Main(string[] args)
        {
            // Optional demonstration (commented out to keep the method side‑effect free).
            // byte[] pdfBytes = File.ReadAllBytes("sample.pdf");
            // byte[] xfdfBytes = PdfAnnotationHelper.ExportAnnotationsToXfdf(pdfBytes);
            // File.WriteAllBytes("output.xfdf", xfdfBytes);
        }
    }
}