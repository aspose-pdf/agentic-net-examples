using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "original.pdf";      // source PDF
        const string backupPath = "original_backup.pdf"; // backup copy
        const string outputPath = "edited.pdf";        // edited PDF

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a backup of the original file (keeps it unchanged)
        File.Copy(inputPath, backupPath, overwrite: true);

        // ---------- Edit PDF using Aspose.Pdf.Facades ----------
        // Create the facade (create rule)
        PdfPageEditor editor = new PdfPageEditor();

        // Load the PDF (load rule)
        editor.BindPdf(inputPath);

        // Example edit: change zoom level
        editor.Zoom = 0.5f;

        // Save edited PDF to a new file (save rule)
        editor.Save(outputPath);

        // Release resources
        editor.Close();

        Console.WriteLine($"Edited PDF saved to '{outputPath}'. Backup created at '{backupPath}'.");
    }
}