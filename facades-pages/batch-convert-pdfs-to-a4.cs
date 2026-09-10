using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the executable (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input / output folders relative to the base directory
        string inputDirectory = Path.Combine(baseDir, "InputPdfs");
        string outputDirectory = Path.Combine(baseDir, "OutputA4");

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Validate the input folder – if it does not exist, fall back to the current working directory
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input folder '{inputDirectory}' not found. Using current directory as fallback.");
            inputDirectory = Directory.GetCurrentDirectory();
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDirectory}'. Operation aborted.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Build output file path (original name with _A4 suffix)
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDirectory, $"{fileName}_A4.pdf");

                // Use PdfPageEditor to change page size to A4
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Load the source PDF
                    editor.BindPdf(inputPath);

                    // Set the desired page size (A4)
                    editor.PageSize = PageSize.A4; // PageSize enum lives in Aspose.Pdf namespace

                    // Process all pages (null means all pages)
                    editor.ProcessPages = null;

                    // Apply the changes and save the result
                    editor.ApplyChanges();
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Converted '{Path.GetFileName(inputPath)}' to A4 -> '{Path.GetFileName(outputPath)}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion to A4 completed.");
    }
}
