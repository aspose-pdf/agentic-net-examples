using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfAnnotationExport
{
    /// <summary>
    /// Provides functionality to export PDF annotations to XFDF using in‑memory streams.
    /// </summary>
    public static class PdfAnnotationExporter
    {
        /// <summary>
        /// Binds a PDF from a memory buffer, exports all annotations to XFDF,
        /// and returns the XFDF data as a byte array.
        /// </summary>
        /// <param name="pdfData">Byte array containing the PDF file.</param>
        /// <returns>Byte array with the exported XFDF content.</returns>
        public static byte[] ExportAnnotationsToXfdf(byte[] pdfData)
        {
            // Input PDF stream (read‑only)
            using (MemoryStream pdfStream = new MemoryStream(pdfData))
            // Facade for annotation operations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF document to the editor
                editor.BindPdf(pdfStream);

                // Output stream that will hold the XFDF data
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    // Export all annotations into the XFDF stream
                    editor.ExportAnnotationsToXfdf(xfdfStream);

                    // Return the XFDF bytes
                    return xfdfStream.ToArray();
                }
            }
        }
    }

    // A minimal console entry point – required for the build runner to execute the assembly.
    // It does not perform any work; it simply exists so the runtime can start the process.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No‑op – the library method can be called from tests or other callers.
        }
    }
}
