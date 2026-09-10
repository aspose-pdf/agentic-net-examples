using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace AsposePdfAsyncDemo
{
    /// <summary>
    /// Provides extension methods for asynchronous PDF text and image extraction.
    /// </summary>
    public static class PdfExtractionExtensions
    {
        /// <summary>
        /// Asynchronously extracts all text from a PDF file and writes it to a text file.
        /// The operation runs on a background thread to keep UI responsive.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF file.</param>
        /// <param name="outputTextPath">Path where the extracted text will be saved.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        public static async Task ExtractTextAsync(string pdfPath, string outputTextPath, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path is required.", nameof(pdfPath));

            if (string.IsNullOrWhiteSpace(outputTextPath))
                throw new ArgumentException("Output text file path is required.", nameof(outputTextPath));

            // Execute the blocking PDF extraction on a thread‑pool thread.
            await Task.Run(() =>
            {
                // Observe cancellation before starting the extraction.
                cancellationToken.ThrowIfCancellationRequested();

                // PdfExtractor implements IDisposable, so wrap it in a using block.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF file to the extractor.
                    extractor.BindPdf(pdfPath);

                    // Extract text using Unicode encoding for maximum fidelity.
                    extractor.ExtractText(Encoding.Unicode);

                    // Save the extracted text to the specified file.
                    extractor.GetText(outputTextPath);
                }
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously extracts all images from a PDF file and saves them to a folder.
        /// Each image is saved as a separate PNG file (default format).
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF file.</param>
        /// <param name="outputFolder">Folder where extracted images will be stored.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        public static async Task ExtractImagesAsync(string pdfPath, string outputFolder, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path is required.", nameof(pdfPath));

            if (string.IsNullOrWhiteSpace(outputFolder))
                throw new ArgumentException("Output folder path is required.", nameof(outputFolder));

            await Task.Run(() =>
            {
                // Check for cancellation before any heavy work.
                cancellationToken.ThrowIfCancellationRequested();

                // Ensure the destination directory exists.
                Directory.CreateDirectory(outputFolder);

                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF file.
                    extractor.BindPdf(pdfPath);

                    // Extract images; default resolution is 150 DPI.
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    // Iterate through all extracted images.
                    while (extractor.HasNextImage())
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                        // Save the current image to disk.
                        extractor.GetNextImage(imagePath);

                        imageIndex++;
                    }
                }
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Minimal console entry point required for compilation. In a real UI application the
    /// asynchronous methods would be called from UI event handlers.
    /// </summary>
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Example usage – replace paths with real files when testing.
            // The demo is intentionally lightweight; it only demonstrates that the
            // async methods can be awaited without blocking the main thread.
            if (args.Length >= 2)
            {
                string pdfPath = args[0];
                string outputPath = args[1];

                try
                {
                    // Extract text.
                    await PdfExtractionExtensions.ExtractTextAsync(pdfPath, outputPath);
                    Console.WriteLine($"Text extracted to: {outputPath}");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Operation was cancelled.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: AsposePdfAsyncDemo <pdfPath> <outputTextPath>");
            }
        }
    }
}
