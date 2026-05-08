using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into the XMP metadata facade, add the custom field,
        // and save the result. The facade implements IDisposable, so we use
        // a using block to ensure proper resource cleanup.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF file
            xmp.BindPdf(inputPath);

            // Add a custom XMP property. The key follows the XMP namespace
            // convention (e.g., "xmp:Department").
            xmp.Add("xmp:Department", "Finance");

            // Save the PDF with the updated XMP metadata.
            xmp.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with Department metadata at '{outputPath}'.");
    }
}