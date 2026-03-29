using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        Aspose.Pdf.Facades.PdfXmpMetadata xmp = new Aspose.Pdf.Facades.PdfXmpMetadata();
        xmp.BindPdf(inputPath);
        xmp.Add("ProjectID", 12345);
        xmp.Save(outputPath);

        Console.WriteLine("Custom XMP metadata added and saved to '" + outputPath + "'.");
    }
}
