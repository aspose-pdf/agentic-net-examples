using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    // Asynchronously flattens annotations for a batch of PDF files.
    // The operation can be cancelled via the provided CancellationToken.
    static async Task FlattenAnnotationsBatchAsync(string[] inputPdfPaths, string outputDirectory, CancellationToken cancellationToken)
    {
        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        foreach (string inputPath in inputPdfPaths)
        {
            // Throw if cancellation was requested before processing the next file.
            cancellationToken.ThrowIfCancellationRequested();

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Derive output file name.
            string outputPath = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(inputPath) + "_flattened.pdf");

            // Use PdfAnnotationEditor to work with annotations.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the source PDF.
                editor.BindPdf(inputPath);

                // Check for cancellation before flattening.
                cancellationToken.ThrowIfCancellationRequested();

                // Perform flattening. This is a synchronous call, but we respect the token.
                editor.FlatteningAnnotations();

                // Check again before saving.
                cancellationToken.ThrowIfCancellationRequested();

                // Save the flattened PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Flattened: {outputPath}");
        }
    }

    static async Task Main(string[] args)
    {
        // Example input PDFs.
        string[] pdfFiles = new[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Output folder for flattened PDFs.
        string outputFolder = "FlattenedOutput";

        // Create a CancellationTokenSource that can be triggered by the user (e.g., pressing a key).
        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            // Optional: cancel on key press.
            Task.Run(() =>
            {
                Console.WriteLine("Press 'c' to cancel the operation...");
                while (Console.ReadKey(true).KeyChar != 'c') { }
                cts.Cancel();
            });

            try
            {
                await FlattenAnnotationsBatchAsync(pdfFiles, outputFolder, cts.Token);
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