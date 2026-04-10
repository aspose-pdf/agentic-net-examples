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

        // Create the XMP metadata facade and bind the PDF file
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);

            // Retrieve the entire XMP metadata as XML bytes
            byte[] metadataBytes = xmp.GetXmpMetadata();

            // Convert to a UTF‑8 string for display (optional)
            string metadataXml = System.Text.Encoding.UTF8.GetString(metadataBytes);
            Console.WriteLine("XMP Metadata:");
            Console.WriteLine(metadataXml);
        }
    }
}