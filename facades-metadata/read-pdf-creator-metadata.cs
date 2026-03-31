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
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            string creator = pdfInfo.Creator;
            Console.WriteLine("Creator: " + creator);
        }
    }
}
