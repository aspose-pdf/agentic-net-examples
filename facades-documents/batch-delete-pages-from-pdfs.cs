using System;
using System.IO;
using Aspose.Pdf.Facades;

class BatchPageDeletion
{
    static void Main()
    {
        // Directory containing PDF files to process
        const string inputDirectory = @"C:\PdfFiles";
        // Directory where the processed PDFs will be saved
        const string outputDirectory = @"C:\PdfFiles\Processed";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Define the pages to delete (1‑based page numbers)
        // Example: delete pages 2 and 4 from every PDF
        int[] pagesToDelete = new int[] { 2, 4 };

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        // Process each PDF file
        foreach (string inputPath in pdfFiles)
        {
            // Build the output file path (same name, different folder)
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputDirectory, fileName);

            try
            {
                // PdfFileEditor performs the delete and saves the result in one call
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

                if (success)
                {
                    Console.WriteLine($"Deleted pages from '{fileName}' → '{outputPath}'");
                }
                else
                {
                    Console.WriteLine($"Failed to delete pages from '{fileName}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}