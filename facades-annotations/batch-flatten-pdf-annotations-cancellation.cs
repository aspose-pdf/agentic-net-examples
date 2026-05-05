using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class AnnotationFlattener
{
    // Asynchronously processes a batch of PDF files, flattening their annotations.
    // The operation can be cancelled via the provided CancellationToken.
    public static async Task ProcessBatchAsync(string inputDirectory, string outputDirectory, CancellationToken cancellationToken)
    {
        if (!Directory.Exists(inputDirectory))
            throw new DirectoryNotFoundException($"Input directory not found: {inputDirectory}");

        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory (non‑recursive for simplicity).
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            // Throw if cancellation was requested before starting the next file.
            cancellationToken.ThrowIfCancellationRequested();

            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputDirectory, fileName);

            // Load, flatten, and save the PDF using Aspose.Pdf.Document.
            // The heavy work is wrapped in Task.Run so the async method does not block the caller.
            await Task.Run(() =>
            {
                // Load the PDF document.
                using (Document pdfDocument = new Document(inputPath))
                {
                    // Use PdfAnnotationEditor to flatten annotations (Document.FlattenAnnotations does not exist).
                    PdfAnnotationEditor editor = new PdfAnnotationEditor();
                    editor.BindPdf(pdfDocument);
                    editor.FlatteningAnnotations();

                    // Save the flattened PDF.
                    pdfDocument.Save(outputPath);
                }
            }, cancellationToken);
        }
    }

    // Example entry point demonstrating cancellation support.
    static async Task Main(string[] args)
    {
        // Example usage:
        // args[0] = input directory, args[1] = output directory.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: AnnotationFlattener <inputDir> <outputDir>");
            return;
        }

        string inputDir = args[0];
        string outputDir = args[1];

        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            // Register a console key press (Ctrl+C) to trigger cancellation.
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true; // Prevent the process from terminating immediately.
                cts.Cancel();
                Console.WriteLine("Cancellation requested...");
            };

            try
            {
                await ProcessBatchAsync(inputDir, outputDir, cts.Token);
                Console.WriteLine("Batch flattening completed successfully.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Batch flattening was cancelled by the user.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
