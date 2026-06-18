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

        // Process each PDF file in the folder
        foreach (string pdfFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            try
            {
                // Initialize the annotation editor facade
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the current PDF file
                    editor.BindPdf(pdfFile);

                    // Delete all annotations in the document
                    editor.DeleteAnnotations();

                    // Overwrite the original file with the cleaned version
                    editor.Save(pdfFile);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }
    }
}