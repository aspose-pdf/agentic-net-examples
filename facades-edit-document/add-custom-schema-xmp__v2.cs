using System;
using System.IO;
using Aspose.Pdf;
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

        // Bind XMP metadata facade to the PDF
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPath);

        // Register a custom namespace for the project schema
        string prefix = "proj";
        string uri = "http://example.com/project";
        xmp.RegisterNamespaceURI(prefix, uri);

        // Add custom fields under the new namespace
        xmp.Add($"{prefix}:Identifier", "Project-XYZ");
        xmp.Add($"{prefix}:Version", "1.2.3");

        // Save the PDF with updated XMP metadata
        xmp.Save(outputPath);

        Console.WriteLine($"Updated XMP metadata saved to '{outputPath}'.");
    }
}