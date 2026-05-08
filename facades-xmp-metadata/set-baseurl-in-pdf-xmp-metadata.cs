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
        const string baseUrl = "https://www.example.com/";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create XMP metadata facade and bind the existing PDF
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Add the BaseURL property to the XMP metadata
            XmpValue urlValue = new XmpValue(baseUrl);
            xmp.Add(DefaultMetadataProperties.BaseURL, urlValue);

            // Save the updated PDF with the new XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"BaseURL set and saved to '{outputPath}'.");
    }
}