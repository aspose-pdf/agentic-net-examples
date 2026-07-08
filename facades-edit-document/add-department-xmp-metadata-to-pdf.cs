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

        // Bind the existing PDF to the XMP metadata facade
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);

        // Add a custom XMP field "Department" with value "Finance"
        xmp.Add("Department", "Finance");

        // Save the PDF with the updated XMP metadata
        xmp.Save(outputPath);

        Console.WriteLine($"PDF saved with XMP metadata at '{outputPath}'.");
    }
}