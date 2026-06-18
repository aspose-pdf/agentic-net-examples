using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class AnnotationFlattener
{
    /// <summary>
    /// Flattens all annotations in a batch of PDF files.
    /// The operation can be cancelled via the provided <see cref="CancellationToken"/>.
    /// </summary>
    /// <param name="inputFiles">Full paths of the PDF files to process.</param>
    /// <param name="outputDirectory">Directory where flattened PDFs will be saved.</param>
    /// <param name="cancellationToken">Token to observe cancellation requests.</param>
    /// <returns>A task that completes when all files have been processed or cancellation is requested.</returns>
    public static async Task FlattenAnnotationsBatchAsync(string[] inputFiles, string outputDirectory, CancellationToken cancellationToken)
    {
        if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
        if (string.IsNullOrWhiteSpace(outputDirectory)) throw new ArgumentException("Output directory must be specified.", nameof(outputDirectory));

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        foreach (string inputPath in inputFiles)
        {
            // Throw if cancellation was requested before starting the next file.
            cancellationToken.ThrowIfCancellationRequested();

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue; // Skip missing files.
            }

            // Derive output file name.
            string outputPath = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(inputPath) + "_flattened.pdf");

            // Load the PDF document using the standard Document constructor (lifecycle rule).
            using (Document doc = new Document(inputPath))
            {
                // Initialize the annotation editor and bind the loaded document.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(doc);

                    // Flatten all annotations in the document.
                    editor.FlatteningAnnotations();

                    // The underlying Document instance now contains the flattened content.
                    // Save the document asynchronously, passing the cancellation token.
                    await doc.SaveAsync(outputPath, cancellationToken).ConfigureAwait(false);
                }
            }

            Console.WriteLine($"Flattened: {inputPath} → {outputPath}");
        }
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console application. It demonstrates how the
    /// <see cref="AnnotationFlattener.FlattenAnnotationsBatchAsync"/> method can be invoked.
    /// The implementation is intentionally minimal – the primary goal is to satisfy
    /// the compiler's requirement for a static Main method.
    /// </summary>
    public static async Task Main(string[] args)
    {
        // Example usage (can be removed or replaced by real arguments).
        // If no arguments are supplied, the program simply exits.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <outputDirectory> <inputFile1> [<inputFile2> ...]");
            return;
        }

        string outputDirectory = args[0];
        string[] inputFiles = args[1..]; // all remaining arguments are input files

        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true; // prevent the process from terminating immediately
            cts.Cancel();
            Console.WriteLine("Cancellation requested…");
        };

        try
        {
            await AnnotationFlattener.FlattenAnnotationsBatchAsync(inputFiles, outputDirectory, cts.Token);
            Console.WriteLine("All files processed.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation was cancelled by the user.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
