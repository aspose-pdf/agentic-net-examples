using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputFolder = "pdfs";
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(pdfPath);
                converter.DoConvert();
                int pageNumber = 1;
                while (converter.HasNextImage())
                {
                    string outputFile = $"{baseName}_page{pageNumber}.jpg";
                    converter.GetNextImage(outputFile);
                    pageNumber++;
                }
            }
        }
    }
}