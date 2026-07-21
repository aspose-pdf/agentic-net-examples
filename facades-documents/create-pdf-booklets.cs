using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the source PDF files
        const string inputDirectory = "InputPdfs";

        // Base directory where each booklet will be placed in its own subfolder
        const string outputBaseDirectory = "BookletOutputs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure the base output directory exists
        Directory.CreateDirectory(outputBaseDirectory);

        // Retrieve all PDF files from the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputFilePath in pdfFiles)
        {
            try
            {
                // Create a dedicated output folder for the current booklet
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFilePath);
                string bookletOutputFolder = Path.Combine(outputBaseDirectory, fileNameWithoutExtension);
                Directory.CreateDirectory(bookletOutputFolder);

                // Define the full path for the generated booklet PDF
                string outputFilePath = Path.Combine(bookletOutputFolder, $"{fileNameWithoutExtension}_booklet.pdf");

                // Use PdfFileEditor from Aspose.Pdf.Facades to create the booklet
                PdfFileEditor pdfEditor = new PdfFileEditor();
                bool result = pdfEditor.MakeBooklet(inputFilePath, outputFilePath);

                if (result)
                {
                    Console.WriteLine($"Booklet created successfully: {outputFilePath}");
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