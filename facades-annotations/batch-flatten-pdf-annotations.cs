using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder to process: first argument or current directory
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

        foreach (string inputPath in pdfFiles)
        {
            // Build output file name with "_flattened" suffix before the extension
            string directory = Path.GetDirectoryName(inputPath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string extension = Path.GetExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_flattened{extension}");

            try
            {
                // Bind the source PDF, flatten its annotations and save the result
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(inputPath);
                    // If you need to remove annotations before flattening, uncomment the next line
                    // editor.DeleteAnnotations();
                    editor.FlatteningAnnotations(); // correct method name
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Flattened: {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}