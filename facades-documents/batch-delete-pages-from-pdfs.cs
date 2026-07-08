using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchPageDeletion
{
    static void Main()
    {
        // Directory containing the PDF files to process
        const string inputDirectory = @"C:\PdfFiles";
        // Directory where the processed PDFs will be saved
        const string outputDirectory = @"C:\PdfFiles\Processed";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Define the pages to delete (1‑based indexing, as required by PdfFileEditor)
        // Example: delete pages 2 and 3 from each document
        int[] pagesToDelete = new int[] { 2, 3 };

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        // Initialize the facade once and reuse it for all files
        PdfFileEditor pdfEditor = new PdfFileEditor();

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Build the output file path – same name, placed in the output folder
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDirectory, fileName + "_trimmed.pdf");

                // Delete the specified pages and save the result
                // Delete returns true on success; we ignore the return value here
                pdfEditor.Delete(inputPath, pagesToDelete, outputPath);

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        // No need to dispose PdfFileEditor (it does not implement IDisposable)
        Console.WriteLine("Batch deletion completed.");
    }
}