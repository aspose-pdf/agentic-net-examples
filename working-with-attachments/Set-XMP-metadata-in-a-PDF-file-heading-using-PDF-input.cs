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

        // Create the XMP metadata facade and bind the existing PDF
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);

        // Register the Dublin Core namespace (required for dc: prefixes)
        xmp.RegisterNamespaceURI("dc", "http://purl.org/dc/elements/1.1/");

        // Add or update the title heading in the XMP metadata
        xmp.Add("dc:title", "Sample Heading Title");

        // Save the PDF with the updated XMP metadata
        xmp.Save(outputPath);

        // Release resources
        xmp.Close();

        Console.WriteLine($"XMP metadata updated and saved to '{outputPath}'.");
    }
}