using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "original.pdf";      // original PDF (unchanged)
        const string backupPath = "original_backup.pdf"; // backup copy
        const string editedPath = "edited.pdf";        // edited output

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Create a backup of the original file (optional, keeps original unchanged)
        File.Copy(sourcePath, backupPath, overwrite: true);

        // Load the PDF into a facade, edit it, and save to a new file
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF (read‑only for the original file)
            editor.BindPdf(sourcePath);

            // Example edit: replace a text occurrence
            editor.ReplaceText("OldText", "NewText");

            // Save the edited document to a separate file
            editor.Save(editedPath);
        }

        Console.WriteLine($"Edited PDF saved to '{editedPath}'. Backup created at '{backupPath}'.");
    }
}