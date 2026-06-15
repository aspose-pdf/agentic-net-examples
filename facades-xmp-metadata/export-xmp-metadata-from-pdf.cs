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

        // Create the PdfXmpMetadata facade, bind the PDF, extract XMP metadata,
        // and write it to a side‑car .xmp file.
        using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata())
        {
            xmpFacade.BindPdf(inputPdf);
            byte[] xmpData = xmpFacade.GetXmpMetadata();
            File.WriteAllBytes(outputXmp, xmpData);
        }

        Console.WriteLine($"XMP metadata exported to '{outputXmp}'.");
    }
}