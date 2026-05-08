using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // original PDF
        const string backupPath = "input_backup.pdf";   // backup copy
        const string outputPath = "output.pdf";         // PDF after annotation deletion

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Create a backup copy of the original PDF before any modifications
            // Overwrite any existing backup to ensure the latest original is saved
            File.Copy(inputPath, backupPath, overwrite: true);
            Console.WriteLine($"Backup created at '{backupPath}'.");

            // Initialize the annotation editor facade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the original PDF (not the backup) for editing
                editor.BindPdf(inputPath);

                // Delete all annotations in the document
                editor.DeleteAnnotations();

                // Save the modified PDF to the output path
                editor.Save(outputPath);
            }

            Console.WriteLine($"Annotations deleted. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}