using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // original PDF
        const string backupPdf  = "input_backup.pdf";   // backup copy
        const string outputPdf  = "output.pdf";         // PDF after annotation deletion

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        try
        {
            // Create a backup of the original PDF before any modifications
            File.Copy(inputPdf, backupPdf, overwrite: true);
            Console.WriteLine($"Backup created at '{backupPdf}'.");

            // Initialize the annotation editor and bind the original PDF
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(inputPdf);

            // Delete all annotations in the document
            editor.DeleteAnnotations();

            // Save the modified PDF to a new file
            editor.Save(outputPdf);
            Console.WriteLine($"Annotations removed. Result saved to '{outputPdf}'.");

            // Release resources held by the facade
            editor.Close();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}