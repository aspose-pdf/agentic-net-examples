using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_branded.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdf = new Document(inputPdf);

        // Register a custom XMP namespace for corporate branding
        // Prefix: "corp", URI: "http://example.com/corporate"
        pdf.Metadata.RegisterNamespaceUri("corp", "http://example.com/corporate");

        // Add custom XMP metadata fields: logo URL and brand color (hex code)
        // Note the "xmp:" prefix before the custom namespace when using the indexer
        pdf.Metadata["xmp:corp:LogoURL"] = "https://example.com/logo.png";
        pdf.Metadata["xmp:corp:BrandColor"] = "#FF5733";

        // Save the PDF with the updated XMP metadata
        pdf.Save(outputPdf);

        Console.WriteLine($"Branded PDF saved to '{outputPdf}'.");
    }
}
