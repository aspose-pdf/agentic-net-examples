using System;
using System.IO;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: DeleteAnnotations <folderPath>");
            return;
        }

        string folderPath = args[0];
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputFileName = "clean_" + fileName; // simple filename, no path

            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfPath);
                editor.DeleteAnnotations();
                editor.Save(outputFileName);
            }

            Console.WriteLine($"Processed {fileName} -> {outputFileName}");
        }
    }
}