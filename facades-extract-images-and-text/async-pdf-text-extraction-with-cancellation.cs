using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main(string[] args)
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "extracted.txt";

        // Create a cancellation source that can be triggered by the user.
        using CancellationTokenSource cts = new CancellationTokenSource();

        // Example: automatically request cancellation after 5 seconds.
        // In a real scenario, this could be a UI button or another event.
        Task.Run(() =>
        {
            Thread.Sleep(5000);
            Console.WriteLine("Cancellation requested by user.");
            cts.Cancel();
        });

        try
        {
            await ExtractTextAsync(inputPdf, outputTxt, cts.Token);
            Console.WriteLine("Text extraction completed successfully.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Text extraction was cancelled.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }

    // Asynchronously extracts text from a PDF using PdfExtractor and respects a cancellation token.
    static async Task ExtractTextAsync(string pdfPath, string outputPath, CancellationToken cancellationToken)
    {
        // Throw immediately if cancellation was already requested.
        cancellationToken.ThrowIfCancellationRequested();

        // PdfExtractor implements IDisposable via Facade, so use a using block.
        using PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF file to the extractor.
        extractor.BindPdf(pdfPath);

        // Optional: define the page range to process (1‑based indexing).
        extractor.StartPage = 1;
        extractor.EndPage = extractor.Document.Pages.Count;

        // Run the potentially long‑running extraction on a background thread.
        await Task.Run(() =>
        {
            // Check cancellation before starting the operation.
            cancellationToken.ThrowIfCancellationRequested();

            // Perform the extraction. This may take noticeable time for large PDFs.
            extractor.ExtractText();

            // Check cancellation again before writing the result.
            cancellationToken.ThrowIfCancellationRequested();

            // Save the extracted text to a file. This uses the extractor's own saving method.
            extractor.GetText(outputPath);
        }, cancellationToken);
    }
}