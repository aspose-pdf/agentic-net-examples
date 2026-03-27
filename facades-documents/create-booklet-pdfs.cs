using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory that contains the source PDF files
        const string inputFolder = "input";
        // Base directory where each booklet will be placed
        const string outputBaseFolder = "output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder '{inputFolder}' does not exist.");
            return;
        }

        // Preserve the original working directory to restore later
        string originalDirectory = Directory.GetCurrentDirectory();

        try
        {
            string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
            foreach (string inputPath in pdfFiles)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputPath);
                string outputDirectory = Path.Combine(outputBaseFolder, fileNameWithoutExtension);
                Directory.CreateDirectory(outputDirectory);

                // Switch to the output directory so that the output file name is simple
                Directory.SetCurrentDirectory(outputDirectory);

                // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.MakeBooklet(inputPath, "booklet.pdf", PageSize.A4);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to create booklet for '{inputPath}'.");
                }
                else
                {
                    Console.WriteLine($"Booklet created: {Path.Combine(outputDirectory, "booklet.pdf")}.");
                }

                // Return to the original directory before processing the next file
                Directory.SetCurrentDirectory(originalDirectory);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Ensure we end in the original working directory
            Directory.SetCurrentDirectory(originalDirectory);
        }
    }
}