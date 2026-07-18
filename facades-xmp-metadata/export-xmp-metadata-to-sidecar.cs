using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF file
        const string outputXmp = "output.xmp";  // side‑car XMP file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Create the PdfXmpMetadata facade, bind it to the PDF,
            // retrieve the XMP metadata as a byte array, and write it to a file.
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(inputPdf);                     // load PDF
                byte[] data = xmp.GetXmpMetadata();        // export XMP
                File.WriteAllBytes(outputXmp, data);       // save side‑car file
            }

            Console.WriteLine($"XMP metadata exported to '{outputXmp}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}