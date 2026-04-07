using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);
        xmp.Add("dc:creator", "John Doe");
        xmp.Add("dc:title", "Project Plan");
        xmp.Save(outputPath);

        Console.WriteLine($"XMP metadata added and saved to '{outputPath}'.");
    }
}