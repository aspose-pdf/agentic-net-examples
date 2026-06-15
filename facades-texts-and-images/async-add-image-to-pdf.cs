using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;   // PdfFileMend resides in this namespace

public static class PdfMendAsyncHelper
{
    /// <summary>
    /// Asynchronously adds an image to the specified page of a PDF document and saves the result.
    /// The operation is executed on a background thread via Task.Run to avoid blocking the caller.
    /// </summary>
    /// <param name="inputPdfPath">Full path to the source PDF file.</param>
    /// <param name="outputPdfPath">Full path where the modified PDF will be saved.</param>
    /// <param name="imagePath">Full path to the image file to be added.</param>
    /// <param name="pageNumber">1‑based page number where the image will be placed.</param>
    /// <param name="llx">Left‑bottom X coordinate of the image rectangle (in points).</param>
    /// <param name="lly">Left‑bottom Y coordinate of the image rectangle (in points).</param>
    /// <param name="urx">Right‑top X coordinate of the image rectangle (in points).</param>
    /// <param name="ury">Right‑top Y coordinate of the image rectangle (in points).</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
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
        // Validate arguments early to avoid unnecessary work.
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));
        if (string.IsNullOrWhiteSpace(outputPdfPath))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));
        if (string.IsNullOrWhiteSpace(imagePath))
            throw new ArgumentException("Image path must be provided.", nameof(imagePath));
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException("Input PDF file not found.", inputPdfPath);
        if (!File.Exists(imagePath))
            throw new FileNotFoundException("Image file not found.", imagePath);
        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be 1 or greater.");

        // Wrap the synchronous PdfFileMend work in Task.Run to keep the method non‑blocking.
        return Task.Run(() =>
        {
            // Observe cancellation request before starting heavy work.
            cancellationToken.ThrowIfCancellationRequested();

            // PdfFileMend implements IDisposable via SaveableFacade, so use a using block.
            using (PdfFileMend mend = new PdfFileMend())
            {
                // Load the source PDF.
                mend.BindPdf(inputPdfPath);

                // Add the image to the specified page.
                // The overload that accepts an array of page numbers allows us to pass a single page.
                mend.AddImage(imagePath, new int[] { pageNumber }, llx, lly, urx, ury);

                // Save the modified document to the target path.
                mend.Save(outputPdfPath);
            }

            // Observe cancellation after the operation completes (optional).
            cancellationToken.ThrowIfCancellationRequested();
        }, cancellationToken);
    }
}

// Added entry point to satisfy the compiler for an executable project.
public class Program
{
    // Async Main is supported from C# 7.1 onward.
    public static async Task Main(string[] args)
    {
        // The console app does not perform any work by default.
        // It exists solely to provide a valid entry point for the project.
        // Example usage (uncomment to test):
        // await PdfMendAsyncHelper.AddImageAsync(
        //     "input.pdf",
        //     "output.pdf",
        //     "image.png",
        //     1,
        //     100f, 100f, 200f, 200f);
    }
}