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
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);
            xmp.Clear();
            xmp.Save(outputPath);
            xmp.Close();
        }

        Console.WriteLine($"XMP metadata cleared. Saved to '{outputPath}'.");
    }
}