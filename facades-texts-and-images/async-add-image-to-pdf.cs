using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    /// <summary>
    /// Contains the asynchronous PDF image‑adding helper.
    /// </summary>
    public static class PdfFileMendExtensions
    {
        /// <summary>
        /// Asynchronously adds an image to a PDF page using <see cref="PdfFileMend"/> and saves the result.
        /// The operation is executed on a background thread via <see cref="Task.Run"/> to avoid blocking the caller.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF file.</param>
        /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
        /// <param name="imagePath">Path to the image file to be added.</param>
        /// <param name="pageNumber">1‑based page number where the image will be placed.</param>
        /// <param name="llx">Left‑bottom X coordinate of the image rectangle.</param>
        /// <param name="lly">Left‑bottom Y coordinate of the image rectangle.</param>
        /// <param name="urx">Upper‑right X coordinate of the image rectangle.</param>
        /// <param name="ury">Upper‑right Y coordinate of the image rectangle.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static Task AddImageAsync(
            string inputPdfPath,
            string outputPdfPath,
            string imagePath,
            int pageNumber,
            float llx,
            float lly,
            float urx,
            float ury,
            CancellationToken cancellationToken = default)
        {
            // Validate arguments early to surface errors before the background work starts.
            if (string.IsNullOrWhiteSpace(inputPdfPath))
                throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));
            if (string.IsNullOrWhiteSpace(outputPdfPath))
                throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentException("Image path must be provided.", nameof(imagePath));
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException("Input PDF not found.", inputPdfPath);
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image file not found.", imagePath);
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be 1 or greater.");

            // Run the PDF modification on a thread‑pool thread.
            return Task.Run(() =>
            {
                // Respect cancellation requests before starting the heavy work.
                cancellationToken.ThrowIfCancellationRequested();

                // PdfFileMend implements IDisposable via its base class SaveableFacade.
                // Use a using block to ensure resources are released promptly.
                using (PdfFileMend mend = new PdfFileMend())
                {
                    // Bind the source PDF file.
                    mend.BindPdf(inputPdfPath);

                    // Add the image to the specified page and coordinates.
                    // The overload expects (string imagePath, int pageNumber, float llx, float lly, float urx, float ury)
                    mend.AddImage(imagePath, pageNumber, llx, lly, urx, ury);

                    // Save the modified document to the output path.
                    mend.Save(outputPdfPath);
                }

                // After the operation completes, check for cancellation again.
                cancellationToken.ThrowIfCancellationRequested();
            }, cancellationToken);
        }
    }

    /// <summary>
    /// Console entry point that demonstrates the asynchronous PDF modification.
    /// </summary>
    public static class Program
    {
        private static async Task ExampleUsageAsync()
        {
            string inputPdf = "sample.pdf";
            string outputPdf = "sample_modified.pdf";
            string imageFile = "logo.png";

            // Coordinates for the image rectangle (example values).
            float llx = 100f, lly = 500f, urx = 300f, ury = 700f;
            int targetPage = 1;

            try
            {
                await PdfFileMendExtensions.AddImageAsync(
                    inputPdf,
                    outputPdf,
                    imageFile,
                    targetPage,
                    llx,
                    lly,
                    urx,
                    ury,
                    CancellationToken.None);

                Console.WriteLine($"Image added successfully. Saved to '{outputPdf}'.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was canceled.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during PDF modification: {ex.Message}");
            }
        }

        // Async Main required for a console application (C# 7.1+).
        public static async Task Main(string[] args)
        {
            await ExampleUsageAsync();
        }
    }
}
