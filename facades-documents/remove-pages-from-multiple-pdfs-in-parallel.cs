using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf.Facades; // PdfFileEditor resides here

namespace PdfUtilities
{
    /// <summary>
    /// Provides functionality to delete specified pages from multiple PDF files in parallel.
    /// </summary>
    public static class PdfPageRemover
    {
        /// <summary>
        /// Deletes the given page numbers from each input PDF and writes the result to the output folder.
        /// </summary>
        /// <param name="inputPdfPaths">Full paths of the source PDF files.</param>
        /// <param name="pagesToRemove">Page numbers to delete (1‑based indexing as required by Aspose.Pdf).</param>
        /// <param name="outputFolder">Folder where the processed PDFs will be saved.</param>
        public static void RemovePagesFromMultipleFiles(
            IEnumerable<string> inputPdfPaths,
            IEnumerable<int> pagesToRemove,
            string outputFolder)
        {
            if (inputPdfPaths == null) throw new ArgumentNullException(nameof(inputPdfPaths));
            if (pagesToRemove == null) throw new ArgumentNullException(nameof(pagesToRemove));
            if (string.IsNullOrWhiteSpace(outputFolder)) throw new ArgumentException("Output folder must be specified.", nameof(outputFolder));

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Convert the page list to an array once – PdfFileEditor expects an int[]
            int[] pagesArray = pagesToRemove.Distinct().OrderBy(p => p).ToArray();

            // Process each file in parallel. Each iteration creates its own PdfFileEditor instance.
            Parallel.ForEach(inputPdfPaths, inputPath =>
            {
                try
                {
                    if (!File.Exists(inputPath))
                    {
                        Console.Error.WriteLine($"Input file not found: {inputPath}");
                        return;
                    }

                    // Build output file name: same name with "_trimmed" suffix
                    string fileName = Path.GetFileNameWithoutExtension(inputPath);
                    string outputPath = Path.Combine(outputFolder, $"{fileName}_trimmed.pdf");

                    // Create a new PdfFileEditor (no IDisposable)
                    PdfFileEditor editor = new PdfFileEditor();

                    // Delete the specified pages and save to the output file.
                    // The Delete method returns a bool indicating success.
                    bool success = editor.Delete(inputPath, pagesArray, outputPath);

                    if (!success)
                    {
                        Console.Error.WriteLine($"Failed to delete pages from: {inputPath}");
                    }
                }
                catch (Exception ex)
                {
                    // Log any unexpected errors per file
                    Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
                }
            });
        }
    }

    // Example usage (can be removed or adapted as needed)
    class Program
    {
        static void Main()
        {
            // Example input PDFs
            var pdfFiles = new List<string>
            {
                @"C:\Docs\Report1.pdf",
                @"C:\Docs\Report2.pdf",
                @"C:\Docs\Report3.pdf"
            };

            // Pages to remove (e.g., remove pages 2 and 5 from each document)
            var pages = new List<int> { 2, 5 };

            // Destination folder for processed PDFs
            string outputDir = @"C:\Docs\Processed";

            // Perform the parallel removal
            PdfPageRemover.RemovePagesFromMultipleFiles(pdfFiles, pages, outputDir);

            Console.WriteLine("Processing completed.");
        }
    }
}