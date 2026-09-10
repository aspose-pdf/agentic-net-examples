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
        /// Sets the Creator metadata of the PDF document using the Facades PdfFileInfo class.
        /// </summary>
        /// <param name="doc">The Aspose.Pdf.Document instance to modify.</param>
        /// <param name="creator">The creator string to assign.</param>
        public static void SetCreator(this Document doc, string creator)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            if (creator == null) throw new ArgumentNullException(nameof(creator));

            // Bind the Facade to the existing Document instance.
            var fileInfo = new PdfFileInfo();
            fileInfo.BindPdf(doc);

            // Set the Creator property.
            fileInfo.Creator = creator;

            // No explicit save is performed here; the caller should save the Document
            // using the standard Document.Save(...) pattern.
        }
    }

    // ---------------------------------------------------------------------
    // A minimal entry point is required because the project is built as a
    // console application.  The Program class does not interfere with the
    // reusable extension method – it merely satisfies the compiler.
    // ---------------------------------------------------------------------
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Example usage (optional, can be removed in production libraries).
            var doc = new Document();
            doc.Pages.Add();
            doc.SetCreator("MyApp Creator");
            // doc.Save("output.pdf"); // Uncomment to persist the file.
        }
    }
}