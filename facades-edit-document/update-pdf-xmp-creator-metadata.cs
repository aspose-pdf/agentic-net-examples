using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newCreator = "My New Creator";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and bind it to the XMP metadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Remove existing creator entry if it exists
            xmp.Remove("dc:creator");

            // Add the new creator value
            xmp.Add("dc:creator", newCreator);

            // Save the PDF with updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"XMP creator updated and saved to '{outputPath}'.");
    }
}