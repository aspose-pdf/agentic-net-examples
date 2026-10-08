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
        /// Adds or updates the CreatorTool metadata entry of the PDF document.
        /// </summary>
        /// <param name="pdfDoc">The Document instance to modify.</param>
        /// <param name="creatorTool">The value to set for the Creator field.</param>
        public static void AddCreatorTool(this Document pdfDoc, string creatorTool)
        {
            if (pdfDoc == null) throw new ArgumentNullException(nameof(pdfDoc));
            if (creatorTool == null) throw new ArgumentNullException(nameof(creatorTool));

            // The Info property holds document metadata. Setting the Creator
            // property writes the /Creator entry in the PDF catalog.
            pdfDoc.Info.Creator = creatorTool;
        }
    }

    // ---------------------------------------------------------------------
    // Minimal entry point required for a console‑style project.
    // The method does not perform any work; it only satisfies the compiler
    // that expects a static Main method.
    // ---------------------------------------------------------------------
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Example (optional) – demonstrates that the extension works.
            // var doc = new Document();
            // doc.AddCreatorTool("MyApp");
            // Console.WriteLine($"Creator set to: {doc.Info.Creator}");
        }
    }
}