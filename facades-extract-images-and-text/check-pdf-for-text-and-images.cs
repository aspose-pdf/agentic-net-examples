using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    public static class PdfContentChecker
    {
        /// <summary>
        /// Returns true if the specified PDF file contains at least one text element and at least one image.
        /// </summary>
        /// <param name="pdfPath">Full path to the PDF file.</param>
        /// <returns>True when both text and images are present; otherwise false.</returns>
        public static bool ContainsTextAndImages(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            // PdfExtractor implements IDisposable, so use a using block for deterministic disposal.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor.
                extractor.BindPdf(pdfPath);

                // ---------- Check for text ----------
                extractor.ExtractText();
                bool hasText;
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);
                    hasText = textStream.Length > 0; // any bytes means text exists
                }

                // ---------- Check for images ----------
                extractor.ExtractImage();
                bool hasImage = extractor.HasNextImage(); // true if at least one image is available

                // Return true only when both text and images are present.
                return hasText && hasImage;
            }
        }
    }

    // Dummy entry point required when the project is built as a console application.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Optional demonstration: pass a PDF path as the first argument.
            if (args.Length > 0)
            {
                string pdfPath = args[0];
                bool containsBoth = PdfContentChecker.ContainsTextAndImages(pdfPath);
                Console.WriteLine($"PDF contains both text and images: {containsBoth}");
            }
        }
    }
}