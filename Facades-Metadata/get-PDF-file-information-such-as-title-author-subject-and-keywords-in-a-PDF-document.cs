using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF metadata using PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            Console.WriteLine($"Title   : {pdfInfo.Title}");
            Console.WriteLine($"Author  : {pdfInfo.Author}");
            Console.WriteLine($"Subject : {pdfInfo.Subject}");
            Console.WriteLine($"Keywords: {pdfInfo.Keywords}");
        }
    }
}