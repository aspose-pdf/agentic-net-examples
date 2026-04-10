using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_branded.pdf";
        const string logoUrl = "https://example.com/logo.png";
        const string brandColor = "#FF5733";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the XMP metadata facade and bind it to the document
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);

            // Register a custom namespace for corporate branding
            xmp.RegisterNamespaceURI("corp", "http://example.com/corporate");

            // Add corporate logo URL and brand color as custom XMP fields
            xmp.Add("corp:LogoURL", logoUrl);
            xmp.Add("corp:BrandColor", brandColor);

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPath}'.");
    }
}