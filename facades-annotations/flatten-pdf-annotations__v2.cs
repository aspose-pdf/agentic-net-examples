using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string folderPath = "pdfs";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Change the current working directory to the target folder so that Save uses simple filenames.
        Directory.SetCurrentDirectory(folderPath);

        string[] pdfFiles = Directory.GetFiles(".", "*.pdf");

        foreach (string pdfFile in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfFile);
            string outputFileName = $"{fileNameWithoutExt}_flattened.pdf";

            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(pdfFile);
                editor.FlatteningAnnotations();
                editor.Save(outputFileName);
                editor.Close();
            }

            Console.WriteLine($"Flattened: {outputFileName}");
        }
    }
}