using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the running application
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve source and destination folders relative to the base directory
        string sourceFolder = Path.Combine(baseDir, "SourcePdfs");
        string destFolder   = Path.Combine(baseDir, "OutputPdfs");

        // Ensure the source folder exists – if it does not, create it and inform the user.
        if (!Directory.Exists(sourceFolder))
        {
            Directory.CreateDirectory(sourceFolder);
            Console.WriteLine($"Source folder not found. Created empty folder at: {sourceFolder}");
            Console.WriteLine("Place PDF files in this folder and re‑run the program.");
            return; // Nothing to process yet.
        }

        // Ensure the destination folder exists
        Directory.CreateDirectory(destFolder);

        // Retrieve all PDF files from the source folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{sourceFolder}'." );
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Preserve the original file name for the output
                string fileName   = Path.GetFileName(inputPath);
                string outputPath = Path.Combine(destFolder, fileName);

                // Use PdfFileEditor (Aspose.Pdf.Facades) to delete pages 3 and 4
                var editor = new PdfFileEditor();

                // Delete returns true on success; handle failure if needed
                bool success = editor.Delete(inputPath, new int[] { 3, 4 }, outputPath);

                if (success)
                {
                    Console.WriteLine($"Processed: {fileName} → {outputPath}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to process: {fileName}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
