using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF and retrieve its XMP metadata as a byte array
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(pdfPath);
            byte[] data = xmp.GetXmpMetadata();

            // Convert the byte array to a UTF‑8 string and display it
            string xml = System.Text.Encoding.UTF8.GetString(data);
            Console.WriteLine("XMP Metadata:");
            Console.WriteLine(xml);
        }
    }
}