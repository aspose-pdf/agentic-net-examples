using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify its XMP metadata, and save the result
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF document
            xmp.BindPdf(inputPath);

            // Example modifications: add/replace XMP properties
            xmp.Add("dc:creator", "John Doe");
            xmp.Add("dc:title",   "Sample Document");
            xmp.Add("dc:description", "Updated XMP metadata via Aspose.Pdf.Facades");

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated XMP metadata to '{outputPath}'.");
    }
}