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

        // Create a new PdfXmpMetadata object (facade for XMP handling)
        PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();

        // Bind the existing PDF file to the XMP metadata instance
        xmpMetadata.BindPdf(inputPath);

        // -----------------------------------------------------------------
        // At this point you can read or modify the XMP packet, e.g.:
        // string currentXmp = xmpMetadata.GetXmpMetadata();
        // xmpMetadata.SetXmpMetadata(modifiedXmp);
        // -----------------------------------------------------------------

        // Save the PDF (with any XMP changes) to a new file
        xmpMetadata.Save(outputPath);

        Console.WriteLine($"PDF with bound XMP metadata saved to '{outputPath}'.");
    }
}