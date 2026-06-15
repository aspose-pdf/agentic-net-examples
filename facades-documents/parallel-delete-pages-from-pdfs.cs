using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class PdfBatchPageDeleter
{
    // Deletes the specified pages from each PDF in the input list in parallel.
    // inputFiles   : full paths of source PDF files.
    // pagesToDelete: page numbers (1‑based) to remove from every file.
    // outputFolder : folder where the processed PDFs will be written.
    public static void DeletePagesParallel(string[] inputFiles, int[] pagesToDelete, string outputFolder)
    {
        if (inputFiles == null) throw new ArgumentNullException(nameof(inputFiles));
        if (pagesToDelete == null) throw new ArgumentNullException(nameof(pagesToDelete));
        if (string.IsNullOrWhiteSpace(outputFolder)) throw new ArgumentException("Output folder must be specified.", nameof(outputFolder));

        Directory.CreateDirectory(outputFolder);

        // Parallel.ForEach creates a separate thread for each iteration.
        Parallel.ForEach(inputFiles, inputPath =>
        {
            try
            {
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    return;
                }

                // Build output file name: original name + "_trimmed.pdf"
                string outputPath = Path.Combine(
                    outputFolder,
                    Path.GetFileNameWithoutExtension(inputPath) + "_trimmed.pdf");

                // Each thread uses its own PdfFileEditor instance (not IDisposable).
                PdfFileEditor editor = new PdfFileEditor();

                // Delete the pages and save to the new file.
                bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

                if (success)
                {
                    Console.WriteLine($"Processed: {inputPath} → {outputPath}");
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

    // Example usage.
    static void Main()
    {
        // List of PDFs to process.
        string[] pdfFiles = new string[]
        {
            @"C:\Docs\Report1.pdf",
            @"C:\Docs\Report2.pdf",
            @"C:\Docs\Report3.pdf"
        };

        // Pages to delete (e.g., remove pages 2 and 3 from each document).
        int[] pagesToRemove = new int[] { 2, 3 };

        // Destination folder for the trimmed PDFs.
        string outputDir = @"C:\Docs\Trimmed";

        DeletePagesParallel(pdfFiles, pagesToRemove, outputDir);
    }
}