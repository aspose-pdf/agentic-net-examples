using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

namespace PdfUtilities
{
    /// <summary>
    /// Provides helper methods for PDF manipulation.
    /// </summary>
    public static class PdfHelper
    {
        /// <summary>
        /// Adds an image to a specific page of a PDF at the given coordinates.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
        /// <param name="pageNumber">1‑based page number where the image will be placed.</param>
        /// <param name="imagePath">Path to the image file to insert.</param>
        /// <param name="llx">Lower‑left X coordinate of the image rectangle (points).</param>
        /// <param name="lly">Lower‑left Y coordinate of the image rectangle (points).</param>
        /// <param name="urx">Upper‑right X coordinate of the image rectangle (points).</param>
        /// <param name="ury">Upper‑right Y coordinate of the image rectangle (points).</param>
        public static void AddImageToPage(
            string inputPdfPath,
            string outputPdfPath,
            int pageNumber,
            string imagePath,
            double llx,
            double lly,
            double urx,
            double ury)
        {
            // ---------------------------------------------------------------------
            // 1. Validate input files
            // ---------------------------------------------------------------------
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException($"PDF not found: {inputPdfPath}");

            if (!File.Exists(imagePath))
                throw new FileNotFoundException($"Image not found: {imagePath}");

            // ---------------------------------------------------------------------
            // 2. Load the PDF and verify the requested page exists
            // ---------------------------------------------------------------------
            using (Document doc = new Document(inputPdfPath))
            {
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    throw new ArgumentOutOfRangeException(nameof(pageNumber),
                        $"Page number must be between 1 and {doc.Pages.Count}.");

                // -----------------------------------------------------------------
                // 3. Create the ImageStamp and configure its size / position
                // -----------------------------------------------------------------
                ImageStamp imgStamp = new ImageStamp(imagePath)
                {
                    // The image should appear on top of existing page content
                    Background = false
                };

                // Width / Height of the rectangle defined by the caller
                float width = (float)(urx - llx);
                float height = (float)(ury - lly);
                imgStamp.Width = width;
                imgStamp.Height = height;

                // Offsets are measured from the lower‑left corner of the page.
                imgStamp.XIndent = (float)llx;
                imgStamp.YIndent = (float)lly;

                // -----------------------------------------------------------------
                // 4. Add the stamp to the requested page and save the document
                // -----------------------------------------------------------------
                Page page = doc.Pages[pageNumber];
                // Use AddStamp instead of Paragraphs.Add – ImageStamp is not a BaseParagraph.
                page.AddStamp(imgStamp);

                doc.Save(outputPdfPath);
            }
        }
    }

    // -------------------------------------------------------------------------
    // A minimal entry point is required for a console‑type project.  The
    // helper class can be used from any other project without invoking Main.
    // -------------------------------------------------------------------------
    internal class Program
    {
        static void Main(string[] args)
        {
            // No default action – the library is intended to be called from
            // user code or unit tests.  Keeping Main empty satisfies the C#
            // compiler when the project type expects an entry point.
        }
    }
}
