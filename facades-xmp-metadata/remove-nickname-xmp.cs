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

        // Load XMP metadata from the PDF
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);

        // Remove the Nickname element
        xmp.Remove(DefaultMetadataProperties.Nickname);

        // Save the updated PDF
        xmp.Save(outputPath);
        Console.WriteLine($"Nickname removed. Saved to '{outputPath}'.");
    }
}