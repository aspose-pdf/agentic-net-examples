using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";        // Original PDF
        const string backupPath = "input_backup.pdf"; // Backup copy
        const string outputPath = "output.pdf";       // Result after deletion

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Create a backup of the original PDF.
        // Overwrite:true ensures an existing backup is replaced.
        // -----------------------------------------------------------------
        File.Copy(inputPath, backupPath, overwrite: true);

        // -----------------------------------------------------------------
        // Step 2: Delete all annotations using PdfAnnotationEditor.
        // The facade is disposed via a using block to guarantee resource release.
        // -----------------------------------------------------------------
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the original PDF (not the backup) for editing.
            editor.BindPdf(inputPath);

            // Remove every annotation in the document.
            editor.DeleteAnnotations();

            // Save the modified document to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Backup created at '{backupPath}'.");
        Console.WriteLine($"Annotations removed; result saved to '{outputPath}'.");
    }
}