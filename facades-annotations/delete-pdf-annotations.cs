using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string folderPath = "pdfs";
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfPath);
                editor.DeleteAnnotations();
                // Overwrite the original file
                editor.Save(pdfPath);
            }
        }

        Console.WriteLine("All annotations have been removed from PDFs in the folder.");
    }
}