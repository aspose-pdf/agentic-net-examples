using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";
        const string backupPdf = "input_backup.pdf";
        const string flattenedPdf = "flattened_output.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Backup the original PDF before any flattening operation
        try
        {
            File.Copy(sourcePdf, backupPdf, true);
            Console.WriteLine($"Backup created at '{backupPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create backup: {ex.Message}");
            return;
        }

        // Flatten annotations using PdfAnnotationEditor
        try
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(sourcePdf);
                editor.FlatteningAnnotations(); // correct method name
                editor.Save(flattenedPdf);
            }
            Console.WriteLine($"Flattened PDF saved to '{flattenedPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Flattening failed: {ex.Message}");
        }
    }
}
