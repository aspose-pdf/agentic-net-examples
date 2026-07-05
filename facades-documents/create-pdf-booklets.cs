using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing the source PDF files
        const string inputDirectory = @"C:\InputPdfs";

        // Base output directory where each booklet will be placed in its own subfolder
        const string outputBaseDirectory = @"C:\BookletOutputs";

        // Validate input directory
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        // Ensure the base output directory exists
        Directory.CreateDirectory(outputBaseDirectory);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Process each PDF file
        foreach (string inputFilePath in pdfFiles)
        {
            try
            {
                // Derive a subfolder name from the source file name (without extension)
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputFilePath);
                string outputFolder = Path.Combine(outputBaseDirectory, fileNameWithoutExt);

                // Create the subfolder for this booklet
                Directory.CreateDirectory(outputFolder);

                // Define the output booklet file path
                string outputFilePath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_booklet.pdf");

                // Use PdfFileEditor to create the booklet
                PdfFileEditor editor = new PdfFileEditor();
                bool result = editor.MakeBooklet(inputFilePath, outputFilePath);

                if (result)
                {
                    Console.WriteLine($"Booklet created: {outputFilePath}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to create booklet for: {inputFilePath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputFilePath}': {ex.Message}");
            }
        }
    }
}