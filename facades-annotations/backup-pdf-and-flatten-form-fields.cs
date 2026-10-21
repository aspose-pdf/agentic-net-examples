using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ---------- Backup the original PDF ----------
        // Backup file name: original name with "_backup" suffix
        string backupPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_backup.pdf");

        try
        {
            // Overwrite any existing backup
            File.Copy(inputPath, backupPath, true);
            Console.WriteLine($"Backup created at '{backupPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create backup: {ex.Message}");
            return;
        }

        // ---------- Flatten the PDF ----------
        try
        {
            // PdfAnnotationEditor provides the FlatteningAnnotations method.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);          // Load the source PDF
                editor.FlatteningAnnotations();    // Correct method to flatten annotations
                editor.Save(outputPath);            // Save the flattened result
            }

            Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Flattening operation failed: {ex.Message}");
        }
    }
}
