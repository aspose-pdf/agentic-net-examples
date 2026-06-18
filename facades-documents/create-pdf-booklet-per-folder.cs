using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Base folder where each booklet will be placed in its own subdirectory
        const string outputBaseFolder = @"C:\BookletOutputs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the base output folder exists
        Directory.CreateDirectory(outputBaseFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Create a subdirectory for this booklet using the source file name (without extension)
                string bookletFolder = Path.Combine(outputBaseFolder, Path.GetFileNameWithoutExtension(pdfPath));
                Directory.CreateDirectory(bookletFolder);

                // Define the output booklet file path
                string outputPath = Path.Combine(bookletFolder, "booklet.pdf");

                // Create the booklet using PdfFileEditor
                PdfFileEditor editor = new PdfFileEditor();
                bool success = editor.MakeBooklet(pdfPath, outputPath);

                if (success)
                {
                    Console.WriteLine($"Booklet created: {outputPath}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to create booklet for: {pdfPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}