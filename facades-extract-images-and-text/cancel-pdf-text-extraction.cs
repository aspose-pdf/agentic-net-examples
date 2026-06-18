using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;   // PdfExtractor resides here

namespace AsposePdfCancellationDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Input PDF and output text file paths
            const string inputPdfPath  = "sample.pdf";
            const string outputTxtPath = "extracted.txt";

            // Create a cancellation token source that can be triggered by the user
            using CancellationTokenSource cts = new CancellationTokenSource();

            // Example: cancel after 5 seconds (replace with any user‑triggered logic)
            Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Console.WriteLine("Cancellation requested.");
                cts.Cancel();
            });

            try
            {
                // Run the extraction operation with cancellation support
                await ExtractTextAsync(inputPdfPath, outputTxtPath, cts.Token);
                Console.WriteLine("Extraction completed.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Extraction was cancelled by the user.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Extracts text from a PDF file using PdfExtractor.
        /// The method checks the supplied <see cref="CancellationToken"/> after each page
        /// and aborts the operation if cancellation is requested.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF.</param>
        /// <param name="outputPath">Path where the extracted text will be saved.</param>
        /// <param name="cancellationToken">Token used to signal cancellation.</param>
        /// <returns>A task representing the asynchronous extraction operation.</returns>
        private static async Task ExtractTextAsync(string pdfPath, string outputPath, CancellationToken cancellationToken)
        {
            // Ensure the source PDF exists
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"Input PDF not found: {pdfPath}");

            // PdfExtractor implements IDisposable via the Facade base class
            using PdfExtractor extractor = new PdfExtractor();

            // Load the PDF – this follows the "load" rule (BindPdf)
            extractor.BindPdf(pdfPath);

            // Prepare the extractor to extract text
            extractor.ExtractText();

            // Open the output file stream – this follows the "save" rule (GetText)
            using FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);

            // Process each page sequentially.
            // After each page we check the cancellation token.
            while (extractor.HasNextPageText())
            {
                // Throw if cancellation was requested
                cancellationToken.ThrowIfCancellationRequested();

                // Write the current page's text to the output stream
                // This uses the PdfExtractor method GetNextPageText(Stream)
                extractor.GetNextPageText(outputStream);
            }

            // Ensure all buffered data is flushed
            await outputStream.FlushAsync(cancellationToken);
        }
    }
}