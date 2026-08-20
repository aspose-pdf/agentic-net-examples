using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_branded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, add corporate branding XMP metadata, and save.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Custom metadata keys – any valid XMP property name can be used.
            xmp.Add("xmp:LogoURL", "https://example.com/logo.png");
            xmp.Add("xmp:BrandColor", "#FF5733"); // Hex color code

            // Persist the changes to a new PDF file.
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPath}'.");
    }
}