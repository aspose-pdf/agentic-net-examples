using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_projectid.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the PDF to the XMP metadata facade
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);

            // Register a custom namespace for the new metadata field
            xmp.RegisterNamespaceURI("proj", "http://example.com/project");

            // Add the custom XMP field ProjectID with value 12345
            xmp.Add("proj:ProjectID", "12345");

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom XMP field at '{outputPath}'.");
    }
}