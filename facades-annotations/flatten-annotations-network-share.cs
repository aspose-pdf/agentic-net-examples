using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // UNC or local network share containing PDF files
        const string networkSharePath = @"\\ServerName\SharedFolder\PDFs";

        if (!Directory.Exists(networkSharePath))
        {
            Console.Error.WriteLine($"Directory not found: {networkSharePath}");
            return;
        }

        // Get all PDF files (including subfolders)
        string[] pdfFiles = Directory.GetFiles(networkSharePath, "*.pdf", SearchOption.AllDirectories);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Create the annotation editor facade
                PdfAnnotationEditor editor = new PdfAnnotationEditor();

                // Load the PDF document
                editor.BindPdf(pdfPath);

                // Flatten all annotations in the document
                editor.FlatteningAnnotations();

                // Overwrite the original file with the flattened version
                editor.Save(pdfPath);

                // Release resources held by the facade
                editor.Close();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Annotation flattening completed for all PDFs.");
    }
}