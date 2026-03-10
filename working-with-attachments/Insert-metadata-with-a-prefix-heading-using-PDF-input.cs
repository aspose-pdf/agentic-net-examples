using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfXmpMetadata facade to manipulate XMP metadata.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF document.
            xmp.BindPdf(inputPdf);

            // Register a custom namespace prefix that will be used for the metadata key.
            // Prefix: "my", Namespace URI: "http://example.com/my"
            xmp.RegisterNamespaceURI("my", "http://example.com/my");

            // Add a custom metadata entry with the registered prefix.
            // The key format is "prefix:propertyName".
            xmp.Add("my:Heading", "Chapter 1");

            // Save the updated PDF with the new metadata.
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"Metadata added and saved to '{outputPdf}'.");
    }
}