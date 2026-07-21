using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace AsposePdfExtensions
{
    public static class PdfExtractionExtensions
    {
        /// <summary>
        /// Asynchronously extracts all text from a PDF file and returns it as a string.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>Extracted text.</returns>
        public static async Task<string> ExtractTextAsync(string pdfPath, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            // Run the blocking PDF extraction on a background thread.
            return await Task.Run(() =>
            {
                // Respect cancellation request before starting.
                cancellationToken.ThrowIfCancellationRequested();

                // PdfExtractor implements IDisposable via Facade, so use using.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF file.
                    extractor.BindPdf(pdfPath);

                    // Perform the extraction.
                    extractor.ExtractText();

                    // Capture the extracted text into a memory stream.
                    using (MemoryStream ms = new MemoryStream())
                    {
                        extractor.GetText(ms);
                        // Reset position before reading.
                        ms.Position = 0;
                        using (StreamReader reader = new StreamReader(ms, Encoding.Unicode))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously extracts all images from a PDF file.
        /// Each image is saved to the specified output directory.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF.</param>
        /// <param name="outputDirectory">Directory where extracted images will be saved.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>List of file paths for the extracted images.</returns>
        public static async Task<IReadOnlyList<string>> ExtractImagesAsync(string pdfPath, string outputDirectory, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));
            if (string.IsNullOrWhiteSpace(outputDirectory))
                throw new ArgumentException("Output directory must be provided.", nameof(outputDirectory));

            // Ensure the output folder exists.
            Directory.CreateDirectory(outputDirectory);

            return await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var savedFiles = new List<string>();
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        string imagePath = Path.Combine(outputDirectory, $"image-{imageIndex}.png");
                        extractor.GetNextImage(imagePath);
                        savedFiles.Add(imagePath);
                        imageIndex++;
                    }
                }
                return (IReadOnlyList<string>)savedFiles;
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously extracts text page‑by‑page and saves each page to a separate text file.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF.</param>
        /// <param name="outputDirectory">Directory where per‑page text files will be saved.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>List of file paths for the generated text files.</returns>
        public static async Task<IReadOnlyList<string>> ExtractTextPerPageAsync(string pdfPath, string outputDirectory, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));
            if (string.IsNullOrWhiteSpace(outputDirectory))
                throw new ArgumentException("Output directory must be provided.", nameof(outputDirectory));

            Directory.CreateDirectory(outputDirectory);

            return await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var pageFiles = new List<string>();
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);
                    extractor.ExtractText();

                    int pageNumber = 1;
                    while (extractor.HasNextPageText())
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        string pageFile = Path.Combine(outputDirectory, $"page-{pageNumber}.txt");
                        extractor.GetNextPageText(pageFile);
                        pageFiles.Add(pageFile);
                        pageNumber++;
                    }
                }
                return (IReadOnlyList<string>)pageFiles;
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // The library methods are intended to be called from other code.
            // This placeholder prevents CS5001 (missing Main) during build.
            await Task.CompletedTask;
        }
    }
}
