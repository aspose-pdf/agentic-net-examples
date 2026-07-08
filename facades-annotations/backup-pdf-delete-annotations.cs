using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // Original PDF
        const string backupPath = "input_backup.pdf";   // Backup copy
        const string outputPath = "input_no_annotations.pdf"; // Result after deletion

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Create a backup of the original PDF (overwrite if it already exists)
            File.Copy(inputPath, backupPath, true);
            Console.WriteLine($"Backup created at '{backupPath}'.");

            // Initialize the annotation editor and bind it to the original PDF
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(inputPath);

            // Delete all annotations in the document
            editor.DeleteAnnotations();

            // Save the modified PDF to a new file (original remains unchanged thanks to the backup)
            editor.Save(outputPath);
            Console.WriteLine($"Annotations removed. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}