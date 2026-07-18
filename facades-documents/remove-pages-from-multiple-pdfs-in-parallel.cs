using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;   // PdfFileEditor resides here

namespace PdfUtilities
{
    /// <summary>
    /// Provides functionality to delete specified pages from multiple PDF files in parallel.
    /// </summary>
    public static class PdfPageRemover
    {
        /// <summary>
        /// Removes the given page numbers from each PDF file supplied.
        /// </summary>
        /// <param name="inputFiles">Full paths of the source PDF files.</param>
        /// <param name="pagesToRemove">Page numbers to delete (1‑based indexing as required by Aspose.Pdf).</param>
        /// <param name="outputDirectory">Directory where the processed PDFs will be saved.</param>
        public static void RemovePagesFromFiles(IEnumerable<string> inputFiles, IEnumerable<int> pagesToRemove, string outputDirectory)
        {
            if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
            if (pagesToRemove == null) throw new ArgumentNullException(nameof(pagesToRemove));
            if (string.IsNullOrWhiteSpace(outputDirectory)) throw new ArgumentException("Output directory must be provided.", nameof(outputDirectory));

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDirectory);

            // Convert the page numbers to an array once – PdfFileEditor expects an int[]
            int[] pagesArray = new List<int>(pagesToRemove).ToArray();

            // Process each file in parallel
            Parallel.ForEach(inputFiles, inputPath =>
            {
                // Validate input file existence
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"Input file not found: {inputPath}");
                    return;
                }

                // Determine output file path (same name with "_trimmed" suffix)
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDirectory, $"{fileName}_trimmed.pdf");

                try
                {
                    // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
                    PdfFileEditor editor = new PdfFileEditor();

                    // Delete the specified pages and save to the output file
                    // Delete returns a bool indicating success; we can log if needed
                    bool success = editor.Delete(inputPath, pagesArray, outputPath);

                    if (success)
                    {
                        Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Failed to delete pages from: {inputPath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
                }
            });
        }
    }

    // Example usage
    class Program
    {
        static void Main()
        {
            // List of PDF files to process
            var pdfFiles = new List<string>
            {
                @"C:\Docs\Report1.pdf",
                @"C:\Docs\Report2.pdf",
                @"C:\Docs\Report3.pdf"
            };

            // Pages to remove (e.g., remove pages 2 and 5 from each document)
            var pages = new List<int> { 2, 5 };

            // Destination folder for the trimmed PDFs
            string outputFolder = @"C:\Docs\Trimmed";

            // Execute the parallel removal
            PdfPageRemover.RemovePagesFromFiles(pdfFiles, pages, outputFolder);
        }
    }
}