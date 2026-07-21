using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfXmpMetadata facade to work with XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Load the PDF file into the facade
            xmp.BindPdf(inputPath);

            // Retrieve the XMP metadata as a byte array
            byte[] data = xmp.GetXmpMetadata();

            // Determine if metadata exists (non‑null and non‑empty)
            bool hasXmp = data != null && data.Length > 0;

            Console.WriteLine(hasXmp
                ? "XMP metadata is present in the PDF."
                : "No XMP metadata found in the PDF.");
        }
    }
}