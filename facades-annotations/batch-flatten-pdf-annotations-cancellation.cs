using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class AnnotationFlattener
{
    /// <summary>
    /// Flattens annotations in a batch of PDF files.
    /// The operation can be cancelled via the provided CancellationToken.
    /// </summary>
    /// <param name="inputFiles">Full paths of source PDFs.</param>
    /// <param name="outputDirectory">Directory where flattened PDFs will be saved.</param>
    /// <param name="cancellationToken">Token to observe for cancellation.</param>
    public static void FlattenAnnotationsBatch(string[] inputFiles, string outputDirectory, CancellationToken cancellationToken)
    {
        if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
        if (string.IsNullOrWhiteSpace(outputDirectory)) throw new ArgumentException("Output directory must be specified.", nameof(outputDirectory));

        Directory.CreateDirectory(outputDirectory);

        // Facade class for annotation operations (does NOT implement IDisposable)
        PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();

        foreach (string inputPath in inputFiles)
        {
            // Check for cancellation before processing each file
            if (cancellationToken.IsCancellationRequested)
                break; // Exit gracefully; caller can handle the partial results

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            string outputPath = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(inputPath) + "_flattened.pdf");

            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Bind the document to the editor
                    annotationEditor.BindPdf(doc);

                    // Flatten all annotations on all pages (correct method name)
                    annotationEditor.FlatteningAnnotations();

                    // Save the flattened PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Flattened: {outputPath}");
            }
            catch (OperationCanceledException)
            {
                // Propagate cancellation if it occurs inside the using block
                Console.WriteLine("Operation cancelled by user.");
                break;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }

    // Example entry point demonstrating usage
    static void Main(string[] args)
    {
        // Example input list; replace with real paths or command‑line parsing as needed
        string[] pdfFiles = new[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        string outputFolder = "FlattenedOutputs";

        // Create a CancellationTokenSource that could be triggered from another thread or UI
        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            // For demonstration, cancel after 10 seconds (remove in real usage)
            // Timer can be replaced with UI button, etc.
            System.Timers.Timer timer = new System.Timers.Timer(10000) { AutoReset = false };
            timer.Elapsed += (s, e) => cts.Cancel();
            timer.Start();

            try
            {
                FlattenAnnotationsBatch(pdfFiles, outputFolder, cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Batch operation was cancelled.");
            }
            finally
            {
                timer.Stop();
                timer.Dispose();
            }
        }
    }
}
