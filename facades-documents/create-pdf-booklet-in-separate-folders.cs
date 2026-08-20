using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = "InputPdfs";
        // Root folder where each booklet will be placed in its own subdirectory
        const string outputRoot = "BookletOutputs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputRoot);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so no using block is needed
        PdfFileEditor editor = new PdfFileEditor();

        foreach (string inputPath in pdfFiles)
        {
            // Create a dedicated output directory for each source PDF
            string fileBaseName = Path.GetFileNameWithoutExtension(inputPath);
            string outputDir = Path.Combine(outputRoot, fileBaseName);
            Directory.CreateDirectory(outputDir);

            // Define the output booklet file path
            string outputPath = Path.Combine(outputDir, "booklet.pdf");

            try
            {
                // Create the booklet; returns true on success
                bool result = editor.MakeBooklet(inputPath, outputPath);
                Console.WriteLine(result
                    ? $"Booklet created successfully: {outputPath}"
                    : $"Failed to create booklet for: {inputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}