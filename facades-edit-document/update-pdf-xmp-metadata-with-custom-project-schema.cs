using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind to the existing PDF and update its XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);

            // Register a custom namespace for project metadata
            const string prefix = "proj";
            const string uri = "http://example.com/project";
            xmp.RegisterNamespaceURI(prefix, uri);

            // Add custom fields under the new namespace
            xmp.Add($"{prefix}:Identifier", "Project-XYZ");
            xmp.Add($"{prefix}:Version", "1.2.3");

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"Updated XMP metadata saved to '{outputPdf}'.");
    }
}