using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = @"C:\InputPdfs";

        // Root folder where each booklet will be placed in its own subdirectory
        const string outputRoot = @"C:\BookletOutputs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output root exists
        Directory.CreateDirectory(outputRoot);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Facade class for file‑level operations (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Derive a subdirectory name from the source file name (without extension)
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string outputDir = Path.Combine(outputRoot, fileNameWithoutExt);
                Directory.CreateDirectory(outputDir);

                // Destination booklet PDF path
                string bookletPath = Path.Combine(outputDir, $"{fileNameWithoutExt}_booklet.pdf");

                // Create the booklet using the correct overload.
                // MakeBooklet returns a bool indicating success.
                bool success = editor.MakeBooklet(pdfPath, bookletPath);
                if (success)
                {
                    Console.WriteLine($"Booklet created: {bookletPath}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to create booklet for '{pdfPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
