using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Bind the XMP metadata facade to the loaded document
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);

            // Define a custom namespace prefix and its URI
            const string customPrefix = "my";
            const string customNamespaceUri = "http://example.com/myNamespace";

            // Register the custom namespace with the XMP metadata object
            xmp.RegisterNamespaceURI(customPrefix, customNamespaceUri);

            // Add a custom metadata entry using the registered prefix
            // The key must be in the form "prefix:propertyName"
            xmp.Add($"{customPrefix}:CustomProperty", "CustomValue");

            // Save the updated PDF (the Save method writes the PDF file with the new metadata)
            xmp.Save(outputPdf);

            // Close the facade (optional, but releases any internal resources)
            xmp.Close();
        }

        Console.WriteLine($"PDF with custom metadata saved to '{outputPdf}'.");
    }
}