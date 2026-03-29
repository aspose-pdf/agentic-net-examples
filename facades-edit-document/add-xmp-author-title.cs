using System;
using System.IO;
using Aspose.Pdf;
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

        using (Document doc = new Document(inputPath))
        {
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);
            xmp.Add("dc:creator", "John Doe");
            xmp.Add("dc:title", "Project Plan");
            xmp.Save(outputPath);
        }

        Console.WriteLine($"XMP metadata added and saved to '{outputPath}'.");
    }
}