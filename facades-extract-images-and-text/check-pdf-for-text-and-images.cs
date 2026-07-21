using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfContentChecker
    {
        /// <summary>
        /// Returns true if the specified PDF file contains at least one piece of text
        /// and at least one image.
        /// </summary>
        /// <param name="pdfPath">Full path to the PDF file.</param>
        /// <returns>True when both text and images are present; otherwise false.</returns>
        public static bool ContainsTextAndImages(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            // PdfExtractor implements IDisposable, so use a using block for deterministic cleanup.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor.
                extractor.BindPdf(pdfPath);

                // Extract text and check if any text pages are available.
                extractor.ExtractText();
                bool hasText = extractor.HasNextPageText();

                // Extract images and check if any images are available.
                extractor.ExtractImage();
                bool hasImage = extractor.HasNextImage();

                // Return true only when both text and images exist.
                return hasText && hasImage;
            }
        }
    }

    // ---------------------------------------------------------------------
    // Minimal entry point required for a console‑application project.
    // The method simply demonstrates usage; it can be removed or replaced
    // when the project is turned into a class‑library.
    // ---------------------------------------------------------------------
    internal class Program
    {
        private static void Main(string[] args)
        {
            // If a PDF path is supplied as the first argument, run the check and write the result.
            if (args.Length > 0)
            {
                string pdfPath = args[0];
                try
                {
                    bool result = PdfContentChecker.ContainsTextAndImages(pdfPath);
                    Console.WriteLine($"PDF contains both text and images: {result}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: AsposePdfApi <pdf-file-path>");
            }
        }
    }
}
