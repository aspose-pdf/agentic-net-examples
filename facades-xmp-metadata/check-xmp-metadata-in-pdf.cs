using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with the XMP metadata facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Retrieve the XMP metadata as a byte array
            byte[] data = xmp.GetXmpMetadata();

            // If the array is non‑null and has length, XMP metadata exists
            bool hasXmp = data != null && data.Length > 0;

            Console.WriteLine(hasXmp
                ? "PDF contains XMP metadata."
                : "PDF does NOT contain XMP metadata.");
        }
    }
}