using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfExtensions
{
    /// <summary>
    /// Extension methods for Aspose.Pdf.Document.
    /// </summary>
    public static class PdfDocumentExtensions
    {
        /// <summary>
        /// Adds or updates the Creator metadata of the PDF document.
        /// </summary>
        /// <param name="pdfDoc">The Aspose.Pdf.Document instance to modify.</param>
        /// <param name="creator">The creator string to set.</param>
        public static void AddCreator(this Document pdfDoc, string creator)
        {
            if (pdfDoc == null) throw new ArgumentNullException(nameof(pdfDoc));
            if (creator == null) throw new ArgumentNullException(nameof(creator));

            // Bind the existing Document to the PdfFileInfo facade.
            PdfFileInfo fileInfo = new PdfFileInfo();
            fileInfo.BindPdf(pdfDoc);

            // Set the Creator property.
            fileInfo.Creator = creator;

            // No explicit Save is required; the change is reflected in the Document object.
        }
    }

    // ---------------------------------------------------------------------
    // Dummy entry point – required because the project is currently built
    // as a console application.  Adding a minimal Main method satisfies the
    // compiler without affecting the reusable extension functionality.
    // ---------------------------------------------------------------------
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Intentionally left blank. The library can be used by referencing
            // this assembly and calling Document.AddCreator(...).
        }
    }
}