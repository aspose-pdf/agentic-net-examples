using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string baseUrl    = "https://www.example.com/";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the existing PDF to the XMP metadata facade
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);

        // Set the BaseURL property in the XMP metadata
        xmp.Add(DefaultMetadataProperties.BaseURL, new XmpValue(baseUrl));

        // Save the PDF with the updated metadata
        xmp.Save(outputPath);
        xmp.Close();

        Console.WriteLine($"BaseURL set to '{baseUrl}' and saved as '{outputPath}'.");
    }
}