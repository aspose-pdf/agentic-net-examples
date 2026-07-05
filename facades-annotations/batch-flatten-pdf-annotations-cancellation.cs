using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // Facades API for annotation handling

class Program
{
    static async Task Main(string[] args)
    {
        // Example input PDF files (could be read from args, config, etc.)
        string[] inputFiles = new[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        string outputDirectory = "FlattenedOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Create a cancellation token source that could be triggered by the user
        using CancellationTokenSource cts = new CancellationTokenSource();

        // For demonstration, cancel after 10 seconds (remove in real usage)
        // Task.Delay(TimeSpan.FromSeconds(10)).ContinueWith(_ => cts.Cancel());

        try
        {
            await FlattenAnnotationsBatchAsync(inputFiles, outputDirectory, cts.Token);
            Console.WriteLine("Batch flattening completed successfully.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Batch flattening was cancelled by the user.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during batch flattening: {ex.Message}");
        }
    }

    /// <summary>
    /// Flattens annotations for a collection of PDF files.
    /// The operation can be cancelled via the provided <see cref="CancellationToken"/>.
    /// </summary>
    /// <param name="inputPaths">Array of source PDF file paths.</param>
    /// <param name="outputDir">Directory where flattened PDFs will be saved.</param>
    /// <param name="cancellationToken">Token to observe cancellation requests.</param>
    /// <returns>A task representing the asynchronous batch operation.</returns>
    public static async Task FlattenAnnotationsBatchAsync(
        string[] inputPaths,
        string outputDir,
        CancellationToken cancellationToken)
    {
        // Run the CPU‑bound work on a background thread to keep the async signature.
        await Task.Run(() =>
        {
            foreach (string inputPath in inputPaths)
            {
                // Throw if cancellation was requested before processing the next file.
                cancellationToken.ThrowIfCancellationRequested();

                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    continue; // Skip missing files, continue with the rest.
                }

                // Determine output file name (same name, different folder).
                string outputPath = Path.Combine(outputDir, Path.GetFileName(inputPath));

                // Use PdfAnnotationEditor (facade) to flatten annotations.
                // The editor implements IDisposable via the base SaveableFacade, so we wrap it in a using block.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the source PDF.
                    editor.BindPdf(inputPath);

                    // Perform flattening of all annotations.
                    editor.FlatteningAnnotations();

                    // Save the modified document.
                    // PdfAnnotationEditor provides a synchronous Save(string) method.
                    // This complies with the lifecycle rule: use the provided save logic.
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Flattened: {inputPath} → {outputPath}");
            }
        }, cancellationToken);
    }
}