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

        // Bind the PDF and add a custom XMP metadata field.
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);
        xmp.Add("ProjectID", "12345");
        xmp.Save(outputPath);

        Console.WriteLine($"Custom XMP metadata added. Saved to '{outputPath}'.");
    }
}