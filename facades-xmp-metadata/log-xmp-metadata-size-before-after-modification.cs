using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_modified.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the XMP metadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);

            // Retrieve XMP metadata before modification
            byte[] beforeData = xmp.GetXmpMetadata();
            Console.WriteLine($"XMP size before modification: {beforeData.Length} bytes");

            // Add a custom XMP property (modification)
            xmp.Add("my:custom", "CustomValue");

            // Retrieve XMP metadata after modification
            byte[] afterData = xmp.GetXmpMetadata();
            Console.WriteLine($"XMP size after modification: {afterData.Length} bytes");

            // Save the PDF with updated XMP metadata
            xmp.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}