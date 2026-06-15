using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the XMP metadata facade and bind the PDF file
        using (PdfXmpMetadata xmpMetadata = new PdfXmpMetadata())
        {
            xmpMetadata.BindPdf(inputPdf);

            // Example: retrieve the entire XMP metadata as a byte array
            byte[] xmpData = xmpMetadata.GetXmpMetadata();

            Console.WriteLine($"XMP metadata retrieved: {xmpData.Length} bytes");
        }
    }
}