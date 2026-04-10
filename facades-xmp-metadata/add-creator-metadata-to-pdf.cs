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
        /// Adds or updates the Creator metadata of the PDF document using the Facades API.
        /// </summary>
        /// <param name="doc">The PDF document to modify.</param>
        /// <param name="creator">The creator string to set.</param>
        public static void AddCreatorTool(this Document doc, string creator)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            if (creator == null) throw new ArgumentNullException(nameof(creator));

            // Use the PdfFileInfo facade to manipulate document metadata.
            PdfFileInfo fileInfo = new PdfFileInfo();

            // Bind the existing Document instance to the facade.
            fileInfo.BindPdf(doc);

            // Set the Creator property.
            fileInfo.Creator = creator;

            // No explicit save is required here; the change is applied to the bound Document.
        }
    }

    // ---------------------------------------------------------------------
    // A minimal entry point is required because the project is built as an
    // executable (Console Application). Adding a no‑op Main method satisfies
    // the compiler without affecting the reusable extension method.
    // ---------------------------------------------------------------------
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Example usage (optional, can be removed in production):
            // var doc = new Document("input.pdf");
            // doc.AddCreatorTool("MyApp v1.0");
            // doc.Save("output.pdf");
        }
    }
}