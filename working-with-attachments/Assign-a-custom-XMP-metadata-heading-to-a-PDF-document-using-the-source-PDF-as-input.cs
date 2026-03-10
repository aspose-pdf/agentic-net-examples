using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        const string inputPdf  = "source.pdf";
        const string outputPdf = "output_with_xmp.pdf";

        // Custom XMP heading value to be added
        const string customHeading = "My Custom Document Heading";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfXmpMetadata (a SaveableFacade) to manipulate XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF document
            xmp.BindPdf(inputPdf);

            // Register a custom namespace prefix (e.g., "custom") with its URI
            // This step is required before adding metadata with that prefix
            xmp.RegisterNamespaceURI("custom", "http://example.com/custom");

            // Add the custom heading under the registered namespace
            // The key format is "prefix:propertyName"
            xmp.Add("custom:Heading", customHeading);

            // Save the modified PDF (including the new XMP metadata) to the output file
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"Custom XMP heading added and saved to '{outputPdf}'.");
    }
}