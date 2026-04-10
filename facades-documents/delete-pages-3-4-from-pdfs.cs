using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Source folder containing PDFs
        const string sourceFolder = @"C:\SourcePdfs";
        // Destination folder for processed PDFs
        const string destinationFolder = @"C:\ProcessedPdfs";

        // Ensure the destination folder exists
        Directory.CreateDirectory(destinationFolder);

        // Verify that the source folder exists before trying to enumerate files
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder does not exist: {sourceFolder}");
            // Optionally create the folder so the program can run without error next time
            // Directory.CreateDirectory(sourceFolder);
            return; // Exit gracefully – nothing to process
        }

        // Get all PDF files in the source folder
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        // Process each PDF file
        foreach (string inputPath in pdfFiles)
        {
            // Build the output file path
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(destinationFolder, fileName);

            // Delete pages 3 and 4 using PdfFileEditor
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Delete(inputPath, new int[] { 3, 4 }, outputPath);

            if (!success)
            {
                Console.Error.WriteLine($"Failed to delete pages for: {inputPath}");
            }
            else
            {
                Console.WriteLine($"Processed: {fileName}");
            }
        }
    }
}
