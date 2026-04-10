using System;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfContentChecker
    {
        /// <summary>
        /// Returns true if the PDF at the specified path contains both text and images.
        /// </summary>
        /// <param name="pdfPath">Full path to the PDF file.</param>
        /// <returns>True when at least one page has text and at least one image exists; otherwise false.</returns>
        public static bool ContainsTextAndImages(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            // PdfExtractor implements IDisposable, so we wrap it in a using block.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF file.
                extractor.BindPdf(pdfPath);

                // Prepare the extractor for text and image enumeration.
                extractor.ExtractText();   // extracts all text using Unicode encoding
                extractor.ExtractImage();  // extracts all images

                // After extraction, these methods indicate whether any text or images are present.
                bool hasText = extractor.HasNextPageText(); // true if at least one page has text
                bool hasImage = extractor.HasNextImage();   // true if at least one image exists

                return hasText && hasImage;
            }
        }
    }

    // Minimal entry point so the project compiles as a console application.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // The library can be used from other projects; no runtime logic is required here.
        }
    }
}