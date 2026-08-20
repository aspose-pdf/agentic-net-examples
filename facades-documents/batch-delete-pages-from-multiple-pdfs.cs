using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the application (portable across environments)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output folders relative to the base directory
        string inputDirectory = Path.Combine(baseDir, "InputPdfs");
        string outputDirectory = Path.Combine(baseDir, "OutputPdfs");

        // If the input folder does not exist, fall back to the current working directory
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input folder not found at '{inputDirectory}'. Using current directory as fallback.");
            inputDirectory = Directory.GetCurrentDirectory();
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Pages to delete (1‑based indexing as required by PdfFileEditor)
        int[] pagesToDelete = new int[] { 2, 3 }; // example: delete pages 2 and 3

        // Process each PDF file in the input directory
        try
        {
            string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
            if (pdfFiles.Length == 0)
            {
                Console.WriteLine($"No PDF files found in '{inputDirectory}'.");
                return;
            }

            foreach (string inputFilePath in pdfFiles)
            {
                string fileName = Path.GetFileName(inputFilePath);
                string outputFilePath = Path.Combine(outputDirectory, fileName);

                // PdfFileEditor does NOT implement IDisposable, so no using block is needed
                PdfFileEditor editor = new PdfFileEditor();

                // Delete the specified pages and save the result to the output path
                bool success = editor.Delete(inputFilePath, pagesToDelete, outputFilePath);

                if (success)
                {
                    Console.WriteLine($"Successfully processed: {fileName}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to process: {fileName}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
