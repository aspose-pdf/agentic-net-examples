using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades; // PdfFileMend resides here

class PdfModifier
{
    /// <summary>
    /// Asynchronously adds an image to the first page of a PDF and saves the result.
    /// All PdfFileMend operations are executed on a background thread via Task.Run
    /// to avoid blocking the calling thread.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF file.</param>
    /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
    /// <param name="imagePath">Path to the image file to be added.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static Task ModifyPdfAsync(
        string inputPdfPath,
        string outputPdfPath,
        string imagePath,
        CancellationToken cancellationToken = default)
    {
        // Validate arguments early
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path is required.", nameof(inputPdfPath));
        if (string.IsNullOrWhiteSpace(outputPdfPath))
            throw new ArgumentException("Output PDF path is required.", nameof(outputPdfPath));
        if (string.IsNullOrWhiteSpace(imagePath))
            throw new ArgumentException("Image path is required.", nameof(imagePath));

        // Run the blocking PdfFileMend work on a thread‑pool thread
        return Task.Run(() =>
        {
            // Throw if cancellation was requested before we start
            cancellationToken.ThrowIfCancellationRequested();

            // Ensure the PDF file exists
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException("Input PDF not found.", inputPdfPath);
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image file not found.", imagePath);

            // Use a using block for deterministic disposal of the facade (lifecycle rule)
            using (PdfFileMend mend = new PdfFileMend())
            {
                // Bind the source PDF file
                mend.BindPdf(inputPdfPath);

                // Open the image stream (will be disposed automatically by the using below)
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    // Add the image to page 1 at the desired rectangle.
                    // Coordinates are in points (1/72 inch). Adjust as needed.
                    // The method returns a bool indicating success; we can ignore it or handle it.
                    bool added = mend.AddImage(imgStream, 1, 10f, 10f, 100f, 100f);
                    if (!added)
                        throw new InvalidOperationException("Failed to add image to PDF.");
                }

                // Save the modified PDF to the output path
                mend.Save(outputPdfPath);

                // Close releases any internal resources (optional because of using)
                mend.Close();
            }
        }, cancellationToken);
    }

    // Example usage
    static async Task Main(string[] args)
    {
        // Example file paths – replace with real paths as needed
        const string inputPdf = "sample_input.pdf";
        const string outputPdf = "sample_output.pdf";
        const string imageFile = "logo.png";

        try
        {
            await ModifyPdfAsync(inputPdf, outputPdf, imageFile);
            Console.WriteLine($"PDF modified and saved to '{outputPdf}'.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation was cancelled.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}