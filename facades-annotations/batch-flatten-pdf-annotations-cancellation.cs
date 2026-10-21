using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class AnnotationFlattener
{
    // Flattens annotations for a batch of PDF files.
    // The operation can be cancelled via the provided CancellationToken.
    public static async Task FlattenBatchAsync(string[] inputPdfPaths, string outputDirectory, CancellationToken cancellationToken)
    {
        if (inputPdfPaths == null) throw new ArgumentNullException(nameof(inputPdfPaths));
        if (string.IsNullOrWhiteSpace(outputDirectory)) throw new ArgumentException("Output directory must be specified.", nameof(outputDirectory));

        Directory.CreateDirectory(outputDirectory);

        foreach (string inputPath in inputPdfPaths)
        {
            // Check for cancellation before processing each file.
            cancellationToken.ThrowIfCancellationRequested();

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            string outputPath = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(inputPath) + "_flattened.pdf");

            try
            {
                // The Aspose.Pdf.Facades API is synchronous; run it on a background thread.
                await Task.Run(() =>
                {
                    // PdfAnnotationEditor does not implement IDisposable in older versions, but using it in a using block is safe for newer releases.
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        // Bind the source PDF, flatten the annotations, and save the result.
                        editor.BindPdf(inputPath);
                        editor.FlatteningAnnotations(); // correct method name
                        editor.Save(outputPath);
                    }
                }, cancellationToken);

                Console.WriteLine($"Flattened: {inputPath} → {outputPath}");
            }
            catch (OperationCanceledException)
            {
                // Propagate cancellation.
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}

class Program
{
    // Entry point required for a console application.
    // Accepts: <outputDirectory> <inputPdf1> [inputPdf2] ...
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <outputDirectory> <inputPdf1> [inputPdf2] ...");
            return;
        }

        string outputDirectory = args[0];
        string[] inputFiles = args.Skip(1).ToArray();

        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (s, e) =>
        {
            cts.Cancel();
            e.Cancel = true; // prevent the process from terminating immediately
        };

        try
        {
            await AnnotationFlattener.FlattenBatchAsync(inputFiles, outputDirectory, cts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Batch flattening was cancelled by the user.");
        }
    }
}