using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            string keywords = pdfInfo.Keywords;
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}