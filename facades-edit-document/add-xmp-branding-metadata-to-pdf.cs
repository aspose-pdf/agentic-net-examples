using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_branded.pdf";

        // Corporate branding values
        const string logoUrl    = "https://example.com/logo.png";
        const string brandColor = "#FF5733";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the XMP metadata facade and bind it to the document
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(doc);

                // Register a custom namespace for branding metadata (prefix "xmp")
                xmp.RegisterNamespaceURI("xmp", "http://ns.adobe.com/xap/1.0/");

                // Add custom XMP properties: logo URL and brand color hex code
                xmp.Add("xmp:LogoURL", logoUrl);
                xmp.Add("xmp:BrandColor", brandColor);

                // Save the PDF with the updated XMP metadata
                xmp.Save(outputPath);
            }
        }

        Console.WriteLine($"Branded PDF saved to '{outputPath}'.");
    }
}