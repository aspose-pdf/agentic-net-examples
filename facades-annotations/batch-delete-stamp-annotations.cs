using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfAnnotationEditor resides here

class Program
{
    static void Main()
    {
        // Folder containing PDF files
        const string inputFolder = @"C:\PdfFiles";
        // Folder to store processed PDFs (can be the same as inputFolder)
        const string outputFolder = @"C:\PdfFiles\Cleaned";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Determine output file name (same name, different folder)
                string fileName = Path.GetFileName(inputPath);
                string outputPath = Path.Combine(outputFolder, fileName);

                // Initialize the annotation editor facade
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Load the PDF document
                    editor.BindPdf(inputPath);

                    // Delete all annotations of type "Stamp"
                    editor.DeleteAnnotations("Stamp");

                    // Save the modified document (overwrites or creates new file)
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch stamp annotation removal completed.");
    }
}