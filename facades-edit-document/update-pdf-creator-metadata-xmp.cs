using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for XMP handling
using Aspose.Pdf;          // XmpValue

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

        // Create the XMP metadata facade and bind the source PDF
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);

        // Add or replace the creator field (dc:creator) in the XMP metadata.
        // DefaultMetadataProperties does not expose a "Creator" constant, so use the literal property name.
        xmp.Add("dc:creator", new XmpValue(newCreator));

        // Save the updated PDF with the modified XMP metadata
        xmp.Save(outputPath);

        // Release resources
        xmp.Close();

        Console.WriteLine($"Creator field updated and saved to '{outputPath}'.");
    }
}
