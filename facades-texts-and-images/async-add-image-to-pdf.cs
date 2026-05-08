using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

public static class PdfModificationHelper
{
    /// <summary>
    /// Asynchronously modifies a PDF by adding an image to the first page.
    /// The modification is performed on a background thread using Task.Run to avoid blocking the caller.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF file.</param>
    /// <param name="imagePath">Path to the image file to be added.</param>
    /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task AddImageAsync(
        string inputPdfPath,
        string imagePath,
        string outputPdfPath,
        CancellationToken cancellationToken = default)
    {
        // Validate input arguments early.
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));
        if (string.IsNullOrWhiteSpace(imagePath))
            throw new ArgumentException("Image path must be provided.", nameof(imagePath));
        if (string.IsNullOrWhiteSpace(outputPdfPath))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException("Input PDF file not found.", inputPdfPath);
        if (!File.Exists(imagePath))
            throw new FileNotFoundException("Image file not found.", imagePath);

        // Run the PDF modification on a thread‑pool thread.
        await Task.Run(() =>
        {
            // The cancellation token is checked before starting the operation.
            cancellationToken.ThrowIfCancellationRequested();

            // PdfFileMend implements IDisposable, so we use a using block for deterministic cleanup.
            using (PdfFileMend mend = new PdfFileMend())
            {
                // Bind the source PDF file.
                mend.BindPdf(inputPdfPath);

                // Add the image to page 1. Coordinates are in default PDF units (points).
                // Example places the image at (100, 500) with a width of 200 and height of 150.
                int pageNumber = 1;               // 1‑based page index
                float llx = 100f;                 // lower‑left X
                float lly = 500f;                 // lower‑left Y
                float urx = llx + 200f;           // upper‑right X
                float ury = lly + 150f;           // upper‑right Y

                mend.AddImage(imagePath, pageNumber, llx, lly, urx, ury);

                // Save the modified document. Save(string) writes a PDF regardless of extension.
                mend.Save(outputPdfPath);
            }
        }, cancellationToken).ConfigureAwait(false);
    }
}

public class Program
{
    // Entry point required for a console‑type project. It can be left empty or used for quick testing.
    public static async Task Main(string[] args)
    {
        // Example usage (uncomment and adjust paths to test):
        // await PdfModificationHelper.AddImageAsync("input.pdf", "image.png", "output.pdf");
    }
}
