using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Base folder where booklet PDFs will be stored
        const string outputBaseFolder = @"C:\BookletOutput";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the base output folder exists
        Directory.CreateDirectory(outputBaseFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Create a subdirectory for each booklet result
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputDir = Path.Combine(outputBaseFolder, fileNameWithoutExt);
                Directory.CreateDirectory(outputDir);

                // Define the output booklet file path
                string outputPath = Path.Combine(outputDir, $"{fileNameWithoutExt}_booklet.pdf");

                // Create the PdfFileEditor and generate the booklet
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.MakeBooklet(inputPath, outputPath);

                if (success)
                {
                    Console.WriteLine($"Booklet created: {outputPath}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to create booklet for: {inputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}