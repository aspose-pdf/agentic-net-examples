using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Width of the console progress bar (number of characters)
    private const int ProgressBarWidth = 50;

    static void Main()
    {
        // Use the application's base directory to build absolute paths – this works cross‑platform
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");

        // Validate input folder – if it does not exist, fall back to the current working directory
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' not found. Using current directory as fallback.");
            inputFolder = Directory.GetCurrentDirectory();
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        int totalFiles = pdfFiles.Length;

        if (totalFiles == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        // PdfAnnotationEditor does NOT implement IDisposable, so we instantiate it once
        PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();

        for (int i = 0; i < totalFiles; i++)
        {
            string inputPath = pdfFiles[i];
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            try
            {
                // Bind the source PDF, delete all annotations, and save the result
                annotationEditor.BindPdf(inputPath);
                annotationEditor.DeleteAnnotations();
                annotationEditor.Save(outputPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
                continue; // skip to next file
            }

            // Update progress bar
            int percent = (i + 1) * 100 / totalFiles;
            DrawProgressBar(percent);
        }

        // Move to the next line after the final progress update
        Console.WriteLine();
        Console.WriteLine("Batch annotation deletion completed.");
    }

    /// <summary>
    /// Renders a simple console progress bar.
    /// </summary>
    /// <param name="percent">Completion percentage (0‑100).</param>
    private static void DrawProgressBar(int percent)
    {
        int filledBars = percent * ProgressBarWidth / 100;
        int emptyBars = ProgressBarWidth - filledBars;
        string bar = new string('█', filledBars) + new string('─', emptyBars);
        Console.Write($"\r[{bar}] {percent,3}%");
    }
}
