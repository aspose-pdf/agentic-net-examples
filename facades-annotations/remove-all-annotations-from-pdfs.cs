using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDF files
        const string folderPath = @"C:\PdfFolder";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Use PdfAnnotationEditor to edit annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the existing PDF file
                    editor.BindPdf(pdfPath);

                    // Delete all annotations in the document
                    editor.DeleteAnnotations();

                    // Overwrite the original file with the cleaned version
                    editor.Save(pdfPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}