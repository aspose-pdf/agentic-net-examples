using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load existing PDF and manipulate its XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Register a custom namespace for the project schema
            const string prefix = "proj";
            const string uri    = "http://example.com/project";
            xmp.RegisterNamespaceURI(prefix, uri);

            // Add custom fields under the new namespace
            xmp.Add($"{prefix}:ProjectID", "MyProject123");
            xmp.Add($"{prefix}:Version",   "1.0.0");

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"Updated XMP metadata saved to '{outputPath}'.");
    }
}