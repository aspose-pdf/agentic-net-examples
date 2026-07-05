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

        // Create the XMP metadata facade and bind the PDF file to it
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Example: retrieve the XMP metadata as XML bytes
            byte[] xmlBytes = xmp.GetXmpMetadata();
            Console.WriteLine($"XMP metadata retrieved, size = {xmlBytes.Length} bytes");
        }
    }
}