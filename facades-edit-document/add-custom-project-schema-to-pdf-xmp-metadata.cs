using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF with XMP metadata
        const string outputPdf = "output.pdf";  // PDF after adding custom schema

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfXmpMetadata facade to manipulate XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF document
            xmp.BindPdf(inputPdf);

            // Register a custom namespace for the project schema
            // Prefix: "proj", URI: "http://example.com/project"
            xmp.RegisterNamespaceURI("proj", "http://example.com/project");

            // Add custom fields under the new namespace
            xmp.Add("proj:Identifier", "Project123");   // Project identifier
            xmp.Add("proj:Version",    "1.0.0");       // Project version number

            // Save the PDF with updated XMP metadata
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"Updated XMP metadata saved to '{outputPdf}'.");
    }
}