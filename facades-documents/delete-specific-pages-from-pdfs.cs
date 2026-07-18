using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string sourceFolder = @"C:\SourcePdfs";
        // Folder where processed PDFs will be saved
        const string destinationFolder = @"C:\ProcessedPdfs";

        // Verify that the source folder exists before attempting to enumerate files
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder does not exist: {sourceFolder}");
            return; // Exit gracefully – nothing to process
        }

        // Ensure the destination folder exists
        Directory.CreateDirectory(destinationFolder);

        // Get all PDF files in the source folder
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf");

        // Page numbers to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 3, 4 };

        foreach (string inputPath in pdfFiles)
        {
            // Build the output file path preserving the original file name
            string outputPath = Path.Combine(destinationFolder, Path.GetFileName(inputPath));

            try
            {
                // Use PdfFileEditor to delete the specified pages and save the result
                PdfFileEditor editor = new PdfFileEditor();
                // Delete returns void – no boolean result to capture
                editor.Delete(inputPath, pagesToDelete, outputPath);

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to delete pages from '{inputPath}': {ex.Message}");
            }
        }
    }
}
