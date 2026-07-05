using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfAsyncExtraction
{
    /// <summary>
    /// Helper class that provides asynchronous PDF text and image extraction using Aspose.Pdf.Facades.PdfExtractor.
    /// All heavy I/O and processing is executed on a background thread via Task.Run to keep UI threads responsive.
    /// </summary>
    public static class PdfExtractionHelper
    {
        /// <summary>
        /// Asynchronously extracts the entire text content of a PDF document and returns it as a string.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF file.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        /// <returns>A task that resolves to the extracted text.</returns>
        public static async Task<string> ExtractTextAsync(string pdfPath, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            return await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var extractor = new PdfExtractor();
                extractor.BindPdf(pdfPath);
                extractor.ExtractText();

                using (var textStream = new MemoryStream())
                {
                    extractor.GetText(textStream, false); // false => do not add BOM
                    textStream.Position = 0;
                    using (var reader = new StreamReader(textStream, Encoding.UTF8))
                    {
                        string result = reader.ReadToEnd();
                        extractor.Close();
                        return result;
                    }
                }
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously extracts the entire text content of a PDF document and writes it to a file.
        /// </summary>
        public static async Task ExtractTextToFileAsync(string pdfPath, string outputPath, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var extractor = new PdfExtractor();
                extractor.BindPdf(pdfPath);
                extractor.ExtractText();
                extractor.GetText(outputPath);
                extractor.Close();
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously extracts all images from a PDF document and returns them as a list of byte arrays.
        /// </summary>
        public static async Task<List<byte[]>> ExtractImagesAsync(string pdfPath, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

            return await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var images = new List<byte[]>();
                var extractor = new PdfExtractor();
                extractor.BindPdf(pdfPath);
                extractor.ExtractImage();

                while (extractor.HasNextImage())
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    using (var imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        images.Add(imgStream.ToArray());
                    }
                }

                extractor.Close();
                return images;
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously extracts all images from a PDF document and saves each image to a specified folder.
        /// </summary>
        public static async Task ExtractImagesToFolderAsync(string pdfPath, string outputFolder, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));
            if (string.IsNullOrWhiteSpace(outputFolder))
                throw new ArgumentException("Output folder must be provided.", nameof(outputFolder));

            await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                Directory.CreateDirectory(outputFolder);
                var extractor = new PdfExtractor();
                extractor.BindPdf(pdfPath);
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    // Save to a temporary file; Aspose will add the correct extension.
                    string tempPath = Path.Combine(outputFolder, $"image-{imageIndex}.tmp");
                    extractor.GetNextImage(tempPath);

                    // Determine final file name with the extension that Aspose actually used.
                    string finalPath = Path.Combine(outputFolder, $"image-{imageIndex}{Path.GetExtension(tempPath)}");
                    if (!string.Equals(tempPath, finalPath, StringComparison.OrdinalIgnoreCase))
                    {
                        File.Move(tempPath, finalPath);
                    }

                    imageIndex++;
                }

                extractor.Close();
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Minimal entry point required for a console application project.
    /// The method is async to allow awaiting any future demo code without blocking.
    /// </summary>
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            // This project is primarily a library; the Main method is kept empty
            // to satisfy the compiler's requirement for an entry point.
            await Task.CompletedTask;
        }
    }
}
