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

        // Create XMP metadata facade, bind it to the source PDF, and update metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Load the PDF document into the facade
            xmp.BindPdf(inputPdf);

            // Register a custom namespace for the project schema
            xmp.RegisterNamespaceURI("proj", "http://example.com/project");

            // Add custom fields using the registered namespace
            xmp.Add("proj:Identifier", "Project123");
            xmp.Add("proj:Version", "1.0.0");

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"Updated XMP metadata saved to '{outputPdf}'.");
    }
}