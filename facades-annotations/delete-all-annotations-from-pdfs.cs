using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files
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
                // PdfAnnotationEditor implements IDisposable via SaveableFacade
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the existing PDF file
                    editor.BindPdf(pdfFile);

                    // Delete all annotations in the document
                    editor.DeleteAnnotations();

                    // Overwrite the original file with the cleaned PDF
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