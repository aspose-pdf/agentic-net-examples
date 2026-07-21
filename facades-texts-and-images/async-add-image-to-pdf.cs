using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;   // Facade API for PDF modifications

namespace PdfAsyncModification
{
    public static class PdfMendExtensions
    {
        /// <summary>
        /// Asynchronously adds an image to a PDF file using PdfFileMend.
        /// The operation is executed on a background thread via Task.Run to avoid blocking the caller.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF file.</param>
        /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
        /// <param name="imagePath">Path to the image file to be added.</param>
        /// <param name="pageNumber">1‑based page number where the image will be placed.</param>
        /// <param name="x">X coordinate (lower‑left) of the image rectangle.</param>
        /// <param name="y">Y coordinate (lower‑left) of the image rectangle.</param>
        /// <param name="width">Width of the image rectangle.</param>
        /// <param name="height">Height of the image rectangle.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static Task AddImageAsync(
            string inputPdfPath,
            string outputPdfPath,
            string imagePath,
            int pageNumber,
            float x,
            float y,
            float width,
            float height,
            CancellationToken cancellationToken = default)
        {
            // Validate arguments early to fail fast.
            if (string.IsNullOrWhiteSpace(inputPdfPath))
                throw new ArgumentException("Input PDF path is required.", nameof(inputPdfPath));
            if (string.IsNullOrWhiteSpace(outputPdfPath))
                throw new ArgumentException("Output PDF path is required.", nameof(outputPdfPath));
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentException("Image path is required.", nameof(imagePath));
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException("Input PDF not found.", inputPdfPath);
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image file not found.", imagePath);
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be 1 or greater.");

            // Wrap the synchronous PdfFileMend work in Task.Run.
            return Task.Run(() =>
            {
                // Respect cancellation request before starting heavy work.
                cancellationToken.ThrowIfCancellationRequested();

                // PdfFileMend does not require a Document instance; it works directly on files.
                // Use a using block to ensure resources are released promptly.
                using (PdfFileMend pdfMend = new PdfFileMend())
                {
                    // Bind the source PDF.
                    pdfMend.BindPdf(inputPdfPath);

                    // Add the image to the specified page and rectangle.
                    // AddImage(string imagePath, int pageNumber, float x, float y, float width, float height)
                    pdfMend.AddImage(imagePath, pageNumber, x, y, width, height);

                    // Save the modified PDF to the output location.
                    pdfMend.Save(outputPdfPath);
                }

                // Throw if cancellation was requested during the operation.
                cancellationToken.ThrowIfCancellationRequested();
            }, cancellationToken);
        }
    }

    // Simple entry point to satisfy the compiler for a console‑style project.
    // The method is async so callers can await asynchronous PDF work if they wish.
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // No mandatory execution logic – the library can be used via the
            // PdfMendExtensions.AddImageAsync method from other code.
            // Example (commented out) of how to call the async method:
            // await PdfMendExtensions.AddImageAsync(
            //     "source.pdf",
            //     "modified.pdf",
            //     "logo.png",
            //     pageNumber: 1,
            //     x: 100,
            //     y: 500,
            //     width: 200,
            //     height: 100);
        }
    }
}
