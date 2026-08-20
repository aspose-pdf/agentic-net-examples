using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;                 // PdfExtractor
using Aspose.Pdf.Multithreading;          // InterruptMonitor

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create an interrupt monitor and make it the thread‑local instance.
        using (InterruptMonitor monitor = new InterruptMonitor())
        {
            InterruptMonitor.ThreadLocalInstance = monitor;

            // Start a background task that waits for a key press and then requests interruption.
            Task.Run(() =>
            {
                Console.WriteLine("Press any key to cancel the extraction...");
                Console.ReadKey(intercept: true);
                monitor.Interrupt(); // Signal cancellation to the running operation.
            });

            // Run the extraction on a separate task so we can observe the cancellation token.
            Task extractionTask = Task.Run(() =>
            {
                // PdfExtractor does not implement IDisposable, but we wrap it in a using block for symmetry.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF file.
                    extractor.BindPdf(inputPdf);

                    // Begin image extraction.
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        // Periodically check the monitor's token; if cancellation was requested,
                        // we break out of the loop after signalling interruption.
                        if (monitor.CancellationToken.IsCancellationRequested)
                        {
                            // The monitor already knows an interrupt was requested via Interrupt(),
                            // but we break to stop further processing.
                            break;
                        }

                        // Save each extracted image to a file.
                        string outputImage = $"image-{imageIndex}.png";
                        extractor.GetNextImage(outputImage);
                        Console.WriteLine($"Saved {outputImage}");
                        imageIndex++;
                    }
                }
            }, monitor.CancellationToken);

            try
            {
                // Wait for the extraction task to finish (or be cancelled).
                extractionTask.Wait();
                Console.WriteLine("Extraction completed.");
            }
            catch (AggregateException ae)
            {
                // If the task was cancelled, handle the OperationCanceledException.
                foreach (var ex in ae.InnerExceptions)
                {
                    if (ex is OperationCanceledException)
                        Console.WriteLine("Extraction was cancelled by the user.");
                    else
                        Console.Error.WriteLine($"Error: {ex.Message}");
                }
            }
            finally
            {
                // Clean up the thread‑local reference.
                InterruptMonitor.ThreadLocalInstance = null;
            }
        }
    }
}