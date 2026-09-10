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

        // Load the PDF and manipulate its XMP metadata using the Facades API
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF document
            xmp.BindPdf(inputPath);

            // Add a custom XMP field "Department" with the value "Finance"
            // The "xmp:" prefix places the entry in the standard XMP namespace
            xmp.Add("xmp:Department", "Finance");

            // Save the updated PDF; the new XMP field will appear in the document properties
            xmp.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with XMP metadata at '{outputPath}'.");
    }
}