using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace PdfUtilities
{
    public static class PdfPageRemover
    {
        /// <summary>
        /// Removes the specified pages from each PDF file in <paramref name="inputFiles"/> in parallel.
        /// The resulting PDFs are saved to <paramref name="outputDirectory"/> preserving the original file name.
        /// </summary>
        /// <param name="inputFiles">Full paths of the source PDF files.</param>
        /// <param name="pagesToRemove">Page numbers to delete (1‑based indexing as required by Aspose.Pdf).</param>
        /// <param name="outputDirectory">Folder where the processed PDFs will be written.</param>
        public static void RemovePagesFromPdfs(IEnumerable<string> inputFiles, IEnumerable<int> pagesToRemove, string outputDirectory)
        {
            if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
            if (pagesToRemove == null) throw new ArgumentNullException(nameof(pagesToRemove));
            if (string.IsNullOrWhiteSpace(outputDirectory)) throw new ArgumentException("Output directory must be provided.", nameof(outputDirectory));

            // Ensure output folder exists
            Directory.CreateDirectory(outputDirectory);

            // Prepare a distinct array of page numbers to delete (Aspose expects an int[])
            int[] pagesArray = pagesToRemove.Distinct().ToArray();

            // Process each PDF in parallel
            Parallel.ForEach(inputFiles, inputFile =>
            {
                try
                {
                    if (!File.Exists(inputFile))
                    {
                        Console.Error.WriteLine($"File not found: {inputFile}");
                        return;
                    }

                    // Build output file path (same name, different folder)
                    string outputPath = Path.Combine(outputDirectory, Path.GetFileName(inputFile));

                    // Use PdfFileEditor.Delete(string, int[], string) – the official Facades API for page removal
                    PdfFileEditor editor = new PdfFileEditor();
                    bool success = editor.Delete(inputFile, pagesArray, outputPath);

                    if (success)
                    {
                        Console.WriteLine($"Processed: {inputFile} → {outputPath}");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Failed to delete pages from: {inputFile}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{inputFile}': {ex.Message}");
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

            // Pages to remove (example: remove pages 2 and 5)
            var pagesToDelete = new List<int> { 2, 5 };

            // Destination folder for the modified PDFs
            string outputFolder = @"C:\Docs\Processed";

            // Execute the removal
            PdfPageRemover.RemovePagesFromPdfs(pdfFiles, pagesToDelete, outputFolder);
        }
    }
}