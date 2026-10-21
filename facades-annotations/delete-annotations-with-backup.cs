using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string backupPath = "input_backup.pdf";

        // Option to keep a backup of the original PDF before modifying it
        bool retainBackup = true;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a backup copy if requested
        if (retainBackup)
        {
            try
            {
                File.Copy(inputPath, backupPath, overwrite: true);
                Console.WriteLine($"Backup created at '{backupPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create backup: {ex.Message}");
                return;
            }
        }

        // Delete all annotations using the Facades API
        try
        {
            // PdfAnnotationEditor does NOT implement IDisposable, so no using block
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Load the source PDF
            editor.BindPdf(inputPath);

            // Remove all annotations from all pages
            editor.DeleteAnnotations();

            // Save the modified PDF
            editor.Save(outputPath);

            Console.WriteLine($"Annotations removed. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during annotation deletion: {ex.Message}");
        }
    }
}