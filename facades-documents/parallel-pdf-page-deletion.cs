using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class ParallelPdfPageDeletion
{
    // Deletes the specified pages from each PDF in the input list concurrently.
    // inputFiles   : full paths of source PDFs
    // pagesToDelete: page numbers (1‑based) to remove from every PDF
    // outputDir    : directory where the processed PDFs will be saved
    public static void DeletePagesFromMultiplePdfs(
        IEnumerable<string> inputFiles,
        int[] pagesToDelete,
        string outputDir)
    {
        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Parallel processing – each file gets its own PdfFileEditor instance
        Parallel.ForEach(inputFiles, inputPath =>
        {
            try
            {
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    return;
                }

                // Build output file name (preserve original name)
                string outputPath = Path.Combine(
                    outputDir,
                    Path.GetFileNameWithoutExtension(inputPath) + "_trimmed.pdf");

                // Each iteration uses a fresh PdfFileEditor (not thread‑safe)
                PdfFileEditor editor = new PdfFileEditor();

                // Delete the pages; method returns true on success
                bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

                if (success)
                {
                    Console.WriteLine($"Processed: {inputPath} → {outputPath}");
                }
                else
                {
                    Console.Error.WriteLine($"Deletion failed for: {inputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });
    }

    // Example usage
    static void Main()
    {
        // List of PDF files to process
        var pdfFiles = new List<string>
        {
            "C:\\Docs\\Report1.pdf",
            "C:\\Docs\\Report2.pdf",
            "C:\\Docs\\Report3.pdf"
        };

        // Pages to delete (e.g., remove pages 2 and 3 from each document)
        int[] pagesToRemove = new int[] { 2, 3 };

        // Destination folder for the resulting PDFs
        string outputFolder = "C:\\Docs\\Processed";

        DeletePagesFromMultiplePdfs(pdfFiles, pagesToRemove, outputFolder);
    }
}