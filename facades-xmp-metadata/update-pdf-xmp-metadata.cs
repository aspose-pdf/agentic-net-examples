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

        // Bind the PDF and modify its XMP metadata using the PdfXmpMetadata facade.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Load the source PDF.
            xmp.BindPdf(inputPath);

            // Add or update a standard XMP property.
            xmp.Add("dc:creator", "John Doe");

            // Register a custom namespace and add a custom property (optional).
            xmp.RegisterNamespaceURI("my", "http://example.com/my");
            xmp.Add("my:customProperty", "Custom value");

            // Save the PDF with the updated XMP metadata.
            xmp.Save(outputPath);
            Console.WriteLine($"PDF saved with updated XMP metadata to '{outputPath}'.");
        }
    }
}
