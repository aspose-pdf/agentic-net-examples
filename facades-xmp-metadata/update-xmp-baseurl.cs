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
        // Simulated request host URL – in a real web service use HttpContext.Current.Request.Url
        const string hostUrl = "https://myservice.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Bind XMP metadata to the document
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);
            // Create XMP value for BaseURL
            XmpValue baseUrlValue = new XmpValue(hostUrl);
            // Add or replace the BaseURL property
            xmp.Add(DefaultMetadataProperties.BaseURL, baseUrlValue);
            // Save the PDF with updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"PDF with updated XMP BaseURL saved to '{outputPath}'.");
    }
}