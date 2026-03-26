using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputDirectory = "pdfs";
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputFile = "cleaned_" + fileName;

            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(pdfPath);
            editor.DeleteAnnotations("Stamp");
            editor.Save(outputFile);
            Console.WriteLine($"Processed '{fileName}' -> '{outputFile}'");
        }
    }
}