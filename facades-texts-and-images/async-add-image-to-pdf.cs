using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

public static class PdfModificationHelper
{
    /// <summary>
    /// Asynchronously adds an image to the first page of a PDF and saves the result.
    /// The heavy PDF work is executed on a background thread via Task.Run to avoid blocking the caller.
    /// </summary>
    /// <param name="inputPdfPath">Full path to the source PDF file.</param>
    /// <param name="imagePath">Full path to the image that will be added.</param>
    /// <param name="outputPdfPath">Full path where the modified PDF will be saved.</param>
    /// <param name="cancellationToken">Optional token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous modification.</returns>
    public static Task AddImageAsync(
        string inputPdfPath,
        string imagePath,
        string outputPdfPath,
        CancellationToken cancellationToken = default)
    {
        // Validate arguments early.
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));
        if (string.IsNullOrWhiteSpace(imagePath))
            throw new ArgumentException("Image path must be provided.", nameof(imagePath));
        if (string.IsNullOrWhiteSpace(outputPdfPath))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

        // Run the synchronous PDF manipulation on a thread‑pool thread.
        return Task.Run(() =>
        {
            // Respect cancellation request before starting heavy work.
            cancellationToken.ThrowIfCancellationRequested();

            // Ensure the source PDF exists.
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException("Source PDF not found.", inputPdfPath);

            // Ensure the image exists.
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image file not found.", imagePath);

            // PdfFileMend is a facade for editing existing PDFs.
            // It implements IDisposable, so we wrap it in a using block.
            using (PdfFileMend mend = new PdfFileMend())
            {
                // Bind the source PDF file to the facade.
                mend.BindPdf(inputPdfPath);

                // Example coordinates (in default PDF units) where the image will be placed.
                // Adjust as needed for your scenario.
                const int targetPage = 1;          // 1‑based page index.
                const float llx = 100f;            // lower‑left X.
                const float lly = 500f;            // lower‑left Y.
                const float urx = 300f;            // upper‑right X.
                const float ury = 700f;            // upper‑right Y.

                // Add the image to the specified page and rectangle.
                // This overload matches the documented signature.
                mend.AddImage(imagePath, targetPage, llx, lly, urx, ury);

                // Save the modified PDF to the desired output location.
                // The Save method writes the file synchronously.
                mend.Save(outputPdfPath);
            }

            // Respect cancellation after the operation as well.
            cancellationToken.ThrowIfCancellationRequested();
        }, cancellationToken);
    }
}

// ---------------------------------------------------------------------------
// Minimal entry point required for a console‑application build.
// The method does not perform any work; it simply exists so the compiler
// can locate a static Main method.  Real usage would call
// PdfModificationHelper.AddImageAsync from an async context.
// ---------------------------------------------------------------------------
public static class Program
{
    public static async Task Main(string[] args)
    {
        // Placeholder – no operation.  Keep the method async to allow
        // awaiting future calls without changing the signature.
        await Task.CompletedTask;
    }
}
