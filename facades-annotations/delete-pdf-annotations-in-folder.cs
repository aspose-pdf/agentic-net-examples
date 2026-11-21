using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Folder containing PDFs – can be supplied as the first argument or edited directly below
        string folderPath = args.Length > 0 ? args[0] : @"C:\Path\To\PdfFolder";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (var pdfPath in pdfFiles)
        {
            try
            {
                // Create the editor, bind the PDF, delete all annotations and save back
                using (var annotationEditor = new PdfAnnotationEditor())
                {
                    annotationEditor.BindPdf(pdfPath);
                    annotationEditor.DeleteAnnotations();
                    annotationEditor.Save(pdfPath);
                }
                Console.WriteLine($"Annotations removed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process {pdfPath}: {ex.Message}");
            }
        }
    }
}
