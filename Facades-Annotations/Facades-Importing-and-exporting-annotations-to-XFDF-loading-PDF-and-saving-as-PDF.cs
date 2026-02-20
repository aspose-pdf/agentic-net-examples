using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input PDF file path
        // 1 - path to export XFDF file
        // 2 - path to import XFDF file (can be the same as export)
        // 3 - output PDF file path (after import)
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <inputPdf> <exportXfdf> <importXfdf> <outputPdf>");
            return;
        }

        string inputPdfPath = args[0];
        string exportXfdfPath = args[1];
        string importXfdfPath = args[2];
        string outputPdfPath = args[3];

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        // Verify that the XFDF file to import exists (if different from export)
        if (!File.Exists(importXfdfPath))
        {
            Console.Error.WriteLine($"Error: XFDF file to import not found at '{importXfdfPath}'.");
            return;
        }

        try
        {
            // Create the annotation editor facade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF document to the editor
                editor.BindPdf(inputPdfPath);

                // Export all annotations to XFDF
                using (FileStream exportStream = File.Create(exportXfdfPath))
                {
                    editor.ExportAnnotationsToXfdf(exportStream);
                }

                // Import annotations from XFDF (null array imports all annotation types)
                using (FileStream importStream = File.OpenRead(importXfdfPath))
                {
                    editor.ImportAnnotationFromXfdf(importStream, null);
                }

                // Save the modified PDF to the specified output path
                editor.Save(outputPdfPath);
            }

            Console.WriteLine("Annotations exported, imported, and PDF saved successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}