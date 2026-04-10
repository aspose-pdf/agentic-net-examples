using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    public static class AnnotationFlattener
    {
        /// <summary>
        /// Flattens annotations for a batch of PDF files.
        /// The operation can be cancelled via the provided <see cref="CancellationToken"/>.
        /// Each input file is processed independently; the output file keeps the original name
        /// and is saved into <paramref name="outputDirectory"/>.
        /// </summary>
        /// <param name="inputPdfPaths">Full paths of the PDFs to process.</param>
        /// <param name="outputDirectory">Directory where flattened PDFs will be written.</param>
        /// <param name="cancellationToken">Token used to cancel the batch operation.</param>
        /// <returns>A task that completes when all files are processed or when cancellation is requested.</returns>
        public static async Task FlattenAnnotationsBatchAsync(
            IEnumerable<string> inputPdfPaths,
            string outputDirectory,
            CancellationToken cancellationToken = default)
        {
            // Ensure the output directory exists.
            Directory.CreateDirectory(outputDirectory);

            // Process each file sequentially. Parallel processing could be added,
            // but cancellation handling is simpler with a sequential loop.
            foreach (string inputPath in inputPdfPaths)
            {
                // Throw if cancellation was requested before starting the next file.
                cancellationToken.ThrowIfCancellationRequested();

                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    continue; // Skip missing files.
                }

                // Derive the output file name.
                string outputPath = Path.Combine(outputDirectory, Path.GetFileName(inputPath));

                // Run the flattening work on a background thread so the caller can cancel.
                await Task.Run(() =>
                {
                    // Respect cancellation while the work is running.
                    cancellationToken.ThrowIfCancellationRequested();

                    // Bind the PDF, flatten annotations, and save.
                    // PdfAnnotationEditor implements IDisposable via SaveableFacade,
                    // so we wrap it in a using block for deterministic cleanup.
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(inputPath);
                        editor.FlatteningAnnotations();
                        editor.Save(outputPath);
                    }
                }, cancellationToken);
            }
        }
    }

    internal class Program
    {
        // Entry point required for a console application. The method is async so callers can await the batch operation.
        private static async Task Main(string[] args)
        {
            // Example usage – replace with real paths or integrate this call elsewhere.
            var inputFiles = new List<string> { "sample1.pdf", "sample2.pdf" };
            string outputDir = "flattened";

            using var cts = new CancellationTokenSource();

            try
            {
                await AnnotationFlattener.FlattenAnnotationsBatchAsync(inputFiles, outputDir, cts.Token);
                Console.WriteLine("Batch flattening completed.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Batch flattening was cancelled.");
            }
        }
    }
}
