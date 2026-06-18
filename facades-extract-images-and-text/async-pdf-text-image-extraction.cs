using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class PdfExtractionHelper
    {
        /// <summary>
        /// Asynchronously extracts all text from a PDF file and saves it to a text file.
        /// The operation is performed on a background thread to keep the UI responsive.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF.</param>
        /// <param name="outputTextPath">Full path where the extracted text will be saved.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        public static async Task ExtractTextAsync(string pdfPath, string outputTextPath, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath)) throw new ArgumentException("PDF path is required.", nameof(pdfPath));
            if (string.IsNullOrWhiteSpace(outputTextPath)) throw new ArgumentException("Output text path is required.", nameof(outputTextPath));

            // Run the extraction on a thread‑pool thread.
            await Task.Run(() =>
            {
                // Ensure the operation can be cancelled.
                cancellationToken.ThrowIfCancellationRequested();

                // PdfExtractor implements IDisposable, so use a using block for deterministic disposal.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF file.
                    extractor.BindPdf(pdfPath);

                    // Extract the text using Unicode encoding (default).
                    extractor.ExtractText();

                    // Save the extracted text to the specified file.
                    // GetText is synchronous, but we are already inside Task.Run.
                    extractor.GetText(outputTextPath);
                }
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously extracts all images from a PDF file and saves them to the specified folder.
        /// Each image is saved with an incremental file name.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF.</param>
        /// <param name="outputFolder">Folder where extracted images will be stored.</param>
        /// <param name="cancellationToken">Optional token to cancel the operation.</param>
        public static async Task ExtractImagesAsync(string pdfPath, string outputFolder, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(pdfPath)) throw new ArgumentException("PDF path is required.", nameof(pdfPath));
            if (string.IsNullOrWhiteSpace(outputFolder)) throw new ArgumentException("Output folder is required.", nameof(outputFolder));

            // Ensure the output directory exists.
            Directory.CreateDirectory(outputFolder);

            await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);

                    // Extract images from the PDF.
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    // Loop while there are more images.
                    while (extractor.HasNextImage())
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        // Build a file name for the current image.
                        string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                        // GetNextImage writes the image to the provided path.
                        // The default image format is PNG; you can change it by using the overload with ImageFormat.
                        extractor.GetNextImage(imagePath);

                        imageIndex++;
                    }
                }
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    class Program
    {
        // Entry point required for an executable project. The method is async so callers can await the helper methods.
        static async Task Main(string[] args)
        {
            // Placeholder implementation – the project builds as a console app.
            // Example usage (uncomment and adjust paths as needed):
            // if (args.Length >= 2)
            // {
            //     string pdf = args[0];
            //     string txt = args[1];
            //     await PdfExtractionHelper.ExtractTextAsync(pdf, txt);
            // }
            await Task.CompletedTask;
        }
    }
}
