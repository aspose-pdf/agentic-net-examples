using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class PdfBatchPageDeleter
{
    /// <summary>
    /// Deletes the specified pages from each PDF in the <paramref name="inputFiles"/> collection
    /// and writes the resulting PDFs to <paramref name="outputDirectory"/>.
    /// Processing is performed in parallel to improve throughput.
    /// </summary>
    /// <param name="inputFiles">Full paths of source PDF files.</param>
    /// <param name="pagesToDelete">Zero‑based page numbers to delete (Aspose.Pdf uses 1‑based indexing, so values are passed directly to the facade).</param>
    /// <param name="outputDirectory">Folder where the processed PDFs will be saved.</param>
    public static void DeletePagesFromMultiplePdfs(IEnumerable<string> inputFiles, int[] pagesToDelete, string outputDirectory)
    {
        if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
        if (pagesToDelete == null) throw new ArgumentNullException(nameof(pagesToDelete));
        if (string.IsNullOrWhiteSpace(outputDirectory)) throw new ArgumentException("Output directory must be specified.", nameof(outputDirectory));

        // Ensure the output folder exists.
        Directory.CreateDirectory(outputDirectory);

        // Parallel processing – each iteration works with its own PdfFileEditor instance.
        Parallel.ForEach(inputFiles, inputPath =>
        {
            // Validate source file existence.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Source file not found: {inputPath}");
                return;
            }

            // Build output file path – keep original file name.
            string outputPath = Path.Combine(outputDirectory, Path.GetFileName(inputPath));

            try
            {
                // Create a new PdfFileEditor for this thread.
                PdfFileEditor editor = new PdfFileEditor();

                // Delete the requested pages and save the result.
                // The Delete method returns a bool indicating success.
                bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

                if (success)
                {
                    Console.WriteLine($"Deleted pages from '{inputPath}' → '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to delete pages from '{inputPath}'.");
                }
            }
            catch (Exception ex)
            {
                // Capture any unexpected errors per file.
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });
    }

    // Example usage.
    static void Main()
    {
        // List of PDF files to process.
        var pdfFiles = new List<string>
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Pages to delete (1‑based as required by the facade).
        int[] pagesToRemove = new int[] { 2, 3 }; // delete pages 2 and 3 from each document.

        // Destination folder for the processed PDFs.
        string outputFolder = "Processed";

        DeletePagesFromMultiplePdfs(pdfFiles, pagesToRemove, outputFolder);
    }
}