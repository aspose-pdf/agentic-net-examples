using System;
using Aspose.Pdf;

namespace PdfExtensions
{
    /// <summary>
    /// Extension methods for Aspose.Pdf.Document.
    /// </summary>
    public static class PdfDocumentExtensions
    {
        /// <summary>
        /// Sets the Creator metadata on an Aspose.Pdf.Document.
        /// The value is persisted when the document is saved.
        /// </summary>
        /// <param name="pdfDoc">The PDF document to modify.</param>
        /// <param name="creator">The creator string to store.</param>
        public static void SetCreator(this Document pdfDoc, string creator)
        {
            if (pdfDoc == null) throw new ArgumentNullException(nameof(pdfDoc));
            // Document.Info is a read‑only property; it is always instantiated by Aspose.Pdf.
            // Directly set the Creator field on the existing DocumentInfo instance.
            pdfDoc.Info.Creator = creator;
        }
    }

    // Minimal entry point to satisfy the compiler when the project is built as an executable.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Intentionally left blank. The library can be used from other projects.
        }
    }
}