using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "original.pdf";      // source PDF to edit
        const string backupPath = "original_backup.pdf"; // backup copy (unchanged)
        const string editedPath = "edited.pdf";        // result after editing

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a backup of the original PDF (keeps the original unchanged)
        File.Copy(inputPath, backupPath, overwrite: true);

        // Use a Facade (PdfContentEditor) to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the original PDF into the facade
            editor.BindPdf(inputPath);

            // Example edit: replace a placeholder string with new text
            // (replace with your own editing logic as needed)
            editor.ReplaceText("PLACEHOLDER", "New Value");

            // Save the edited document to a new file.
            // The original file remains untouched; the backup preserves it.
            editor.Save(editedPath);
        }

        Console.WriteLine($"Backup created at '{backupPath}'.");
        Console.WriteLine($"Edited PDF saved at '{editedPath}'.");
    }
}