using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xmpLogPath   = "original_xmp.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Bind the PDF to the XMP metadata facade
        PdfXmpMetadata xmpFacade = new PdfXmpMetadata();
        xmpFacade.BindPdf(inputPdfPath);

        // Retrieve the XMP metadata as a byte array (XML format)
        byte[] xmpData = xmpFacade.GetXmpMetadata();

        // Persist the original XMP XML for audit purposes
        File.WriteAllBytes(xmpLogPath, xmpData);
        Console.WriteLine($"Original XMP metadata saved to '{xmpLogPath}'.");
    }
}