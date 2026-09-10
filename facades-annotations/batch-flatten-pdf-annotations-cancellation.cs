using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchAnnotationFlattener
{
    /// <summary>
    /// Flattens annotations in all PDF files found in <paramref name="inputDirectory"/> and writes the results to <paramref name="outputDirectory"/>.
    /// The operation can be cancelled via <paramref name="cancellationToken"/>.
    /// </summary>
    /// <param name="inputDirectory">Folder containing source PDF files.</param>
    /// <param name="outputDirectory">Folder where flattened PDFs will be saved.</param>
    /// <param name="cancellationToken">Token to observe for cancellation.</param>
    public static void FlattenAnnotationsInFolder(string inputDirectory, string outputDirectory, CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(inputDirectory))
            throw new DirectoryNotFoundException($"Input directory not found: {inputDirectory}");

        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files (non‑recursive for simplicity)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string sourcePath in pdfFiles)
        {
            // Observe cancellation before processing each file
            cancellationToken.ThrowIfCancellationRequested();

            string fileName = Path.GetFileName(sourcePath);
            string destPath = Path.Combine(outputDirectory, fileName);

            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(sourcePath))
            {
                // Initialize the annotation editor facade
                PdfAnnotationEditor editor = new PdfAnnotationEditor();
                editor.BindPdf(doc); // Bind the in‑memory document

                // Flatten all annotations (no custom settings needed)
                editor.FlatteningAnnotations();

                // Save the modified document (lifecycle rule: use Save)
                editor.Save(destPath);

                // Close the editor (Dispose is optional but good practice)
                editor.Close();
            }

            Console.WriteLine($"Flattened: {fileName} → {destPath}");
        }
    }
}

// Example usage
class Program
{
    static void Main()
    {
        string inputFolder = @"C:\InputPdfs";
        string outputFolder = @"C:\FlattenedPdfs";

        // Create a CancellationTokenSource that could be triggered elsewhere
        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            try
            {
                // Start the batch flattening operation
                BatchAnnotationFlattener.FlattenAnnotationsInFolder(inputFolder, outputFolder, cts.Token);
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