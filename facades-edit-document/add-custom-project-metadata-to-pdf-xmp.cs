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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the XMP metadata facade and bind it to the existing PDF
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);

            // Register a custom namespace for project metadata
            const string prefix = "proj";
            const string uri    = "http://example.com/project";
            xmp.RegisterNamespaceURI(prefix, uri);

            // Add custom fields under the new namespace
            xmp.Add($"{prefix}:ProjectID", "PRJ-001");
            xmp.Add($"{prefix}:Version",   "1.0");

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"Updated XMP metadata saved to '{outputPdf}'.");
    }
}