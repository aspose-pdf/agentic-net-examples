using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string backupPath = "input_backup.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a backup copy of the original PDF
        File.Copy(inputPath, backupPath, true);

        // Delete all annotations using PdfAnnotationEditor
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);
        editor.DeleteAnnotations();
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Backup created at '{backupPath}'. Annotations removed, result saved to '{outputPath}'.");
    }
}
