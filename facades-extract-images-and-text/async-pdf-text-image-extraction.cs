using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfAsyncProcessing
{
    public static class PdfExtractionExtensions
    {
        // Asynchronously extracts all text from a PDF and saves it to a text file.
        // The method runs the blocking Aspose.Pdf calls on a background thread to keep the UI responsive.
        public static async Task ExtractTextAsync(string pdfPath, string outputTextPath, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath)) throw new ArgumentException("PDF path is required.", nameof(pdfPath));
            if (string.IsNullOrWhiteSpace(outputTextPath)) throw new ArgumentException("Output text path is required.", nameof(outputTextPath));

            // Ensure the PDF file exists before starting the operation.
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            await Task.Run(() =>
            {
                // Respect cancellation requests.
                cancellationToken.ThrowIfCancellationRequested();

                // Use the recommended lifecycle: wrap Document in a using block.
                using (Document doc = new Document(pdfPath))
                {
                    // PdfExtractor is a facade; it does not implement IDisposable, but we still dispose the Document.
                    PdfExtractor extractor = new PdfExtractor();
                    extractor.BindPdf(doc);
                    extractor.ExtractText(); // Synchronous extraction.

                    // Save extracted text to the specified file.
                    extractor.GetText(outputTextPath);
                }
            }, cancellationToken).ConfigureAwait(false);
        }

        // Asynchronously extracts all images from a PDF and saves them to the specified directory.
        // Each image is saved with an incremental name (image-1.png, image-2.png, ...).
        public static async Task ExtractImagesAsync(string pdfPath, string outputDirectory, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath)) throw new ArgumentException("PDF path is required.", nameof(pdfPath));
            if (string.IsNullOrWhiteSpace(outputDirectory)) throw new ArgumentException("Output directory is required.", nameof(outputDirectory));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            // Create the output folder if it does not exist.
            Directory.CreateDirectory(outputDirectory);

            await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (Document doc = new Document(pdfPath))
                {
                    PdfExtractor extractor = new PdfExtractor();
                    extractor.BindPdf(doc);
                    extractor.ExtractImage(); // Synchronous image extraction.

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        // Build a file name for each extracted image.
                        string imagePath = Path.Combine(outputDirectory, $"image-{imageIndex}.png");
                        // GetNextImage saves the image to the provided path.
                        extractor.GetNextImage(imagePath);
                        imageIndex++;
                    }
                }
            }, cancellationToken).ConfigureAwait(false);
        }

        // Example of extracting text page‑by‑page asynchronously.
        // Each page's text is saved to a separate file with a prefix and numeric suffix.
        public static async Task ExtractTextPerPageAsync(string pdfPath, string outputFolder, string filePrefix, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath)) throw new ArgumentException("PDF path is required.", nameof(pdfPath));
            if (string.IsNullOrWhiteSpace(outputFolder)) throw new ArgumentException("Output folder is required.", nameof(outputFolder));
            if (string.IsNullOrWhiteSpace(filePrefix)) throw new ArgumentException("File prefix is required.", nameof(filePrefix));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            Directory.CreateDirectory(outputFolder);

            await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (Document doc = new Document(pdfPath))
                {
                    PdfExtractor extractor = new PdfExtractor();
                    extractor.BindPdf(doc);
                    extractor.ExtractText(Encoding.Unicode); // Use Unicode encoding for better character support.

                    int pageNumber = 1;
                    while (extractor.HasNextPageText())
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        string pageFile = Path.Combine(outputFolder, $"{filePrefix}{pageNumber}.txt");
                        extractor.GetNextPageText(pageFile);
                        pageNumber++;
                    }
                }
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    // Entry point required for a console‑style project.
    // The Main method is async so that callers can await the extraction methods if they wish.
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            // No operation – the library methods are intended to be called from UI or other code.
            // Keeping an async Main satisfies the compiler while preserving the original behaviour.
            await Task.CompletedTask;
        }
    }
}
