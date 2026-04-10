using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Define the folder that contains the source PDF files
        const string inputFolder = @"C:\InputPdfs";

        // Define the base folder where each booklet will be saved
        const string outputBaseFolder = @"C:\BookletOutputs";

        // Ensure the output base folder exists
        Directory.CreateDirectory(outputBaseFolder);

        // Verify that the input folder exists before trying to enumerate files
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            Console.WriteLine("Please create the folder and place PDF files inside before running the program.");
            return; // Exit gracefully – no unhandled exception
        }

        // Get all PDF files from the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in: {inputFolder}");
            return;
        }

        // Process each PDF file
        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Derive a folder name from the input file name (without extension)
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputFolder = Path.Combine(outputBaseFolder, fileNameWithoutExt);

                // Create the output folder for this booklet
                Directory.CreateDirectory(outputFolder);

                // Define the output booklet file path
                string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_booklet.pdf");

                // Create a PdfFileEditor instance (facade) and generate the booklet
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.MakeBooklet(inputPath, outputPath);

                // Report the result
                Console.WriteLine(success
                    ? $"Booklet created: {outputPath}"
                    : $"Failed to create booklet for: {inputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
