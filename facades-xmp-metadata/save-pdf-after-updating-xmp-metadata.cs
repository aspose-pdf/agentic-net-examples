using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the XMP metadata facade, bind the PDF, modify metadata, and save
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Load the PDF into the facade
            xmp.BindPdf(inputPath);

            // Add or update XMP properties (key, value). Use string overload to avoid needing Aspose.Pdf.Xmp namespace.
            xmp.Add("dc:creator", "John Doe");
            xmp.Add("dc:title",   "Sample Document");

            // Save the PDF with the updated XMP metadata
            xmp.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated XMP metadata to '{outputPath}'.");
    }
}
