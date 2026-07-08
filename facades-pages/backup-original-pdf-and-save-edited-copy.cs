using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "original.pdf";
        const string backupPath = "original_backup.pdf";
        const string editedPath = "edited.pdf";

        // Verify the source PDF exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Create a backup of the original file (keeps it unchanged)
        try
        {
            File.Copy(sourcePath, backupPath, overwrite: true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create backup: {ex.Message}");
            return;
        }

        // Use the PdfContentEditor facade to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the original PDF into the facade
            editor.BindPdf(sourcePath);

            // Example edit (optional): replace a placeholder text
            // editor.ReplaceText("PLACEHOLDER", "New Text");

            // Save the edited PDF to a new file; the original remains untouched
            editor.Save(editedPath);
        }

        Console.WriteLine($"Backup created at '{backupPath}'. Edited PDF saved at '{editedPath}'.");
    }
}