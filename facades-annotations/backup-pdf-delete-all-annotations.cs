using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string backupPath = "input_backup.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Retain a backup copy of the original PDF
            File.Copy(inputPath, backupPath, true);
            Console.WriteLine($"Backup created at '{backupPath}'.");

            // Delete all annotations using PdfAnnotationEditor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);
                editor.DeleteAnnotations();          // remove all annotations
                editor.Save(outputPath);              // save the modified PDF
            }

            Console.WriteLine($"Annotations deleted. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}