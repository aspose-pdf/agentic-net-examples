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
        /// Adds or updates the Creator metadata of the PDF document.
        /// </summary>
        /// <param name="document">The Aspose.Pdf.Document instance to modify.</param>
        /// <param name="creator">The creator string to set.</param>
        public static void SetCreator(this Document document, string creator)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            if (creator == null) throw new ArgumentNullException(nameof(creator));

            // Aspose.Pdf provides a direct way to edit document metadata via the Info property.
            // No facade or additional Save call is required – the changes are kept in memory
            // and will be persisted when the Document is saved by the caller.
            document.Info.Creator = creator;
        }
    }

    // A minimal entry point is required for a console‑application project.
    // It does not interfere with the reusable extension; it simply satisfies the compiler.
    internal class Program
    {
        static void Main(string[] args)
        {
            // Example (optional) – demonstrates that the extension compiles and works.
            // var doc = new Document();
            // doc.SetCreator("My Application");
            // doc.Save("output.pdf");
        }
    }
}