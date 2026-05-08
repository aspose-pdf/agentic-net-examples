using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXmp = "output.xmp";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF file and extract its XMP metadata as a byte array
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);
            byte[] data = xmp.GetXmpMetadata();

            // Write the metadata bytes to a side‑car .xmp file
            File.WriteAllBytes(outputXmp, data);
        }

        Console.WriteLine($"XMP metadata exported to '{outputXmp}'.");
    }
}