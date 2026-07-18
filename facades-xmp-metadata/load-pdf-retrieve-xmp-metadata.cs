using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify that the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create a PdfXmpMetadata facade instance
        PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();

        // Bind the existing PDF file to the facade
        xmpMetadata.BindPdf(pdfPath);

        // Retrieve the entire XMP metadata as a byte array
        byte[] xmpBytes = xmpMetadata.GetXmpMetadata();

        // Convert the byte array to a UTF‑8 string for display (optional)
        string xmpXml = Encoding.UTF8.GetString(xmpBytes);
        Console.WriteLine("XMP Metadata:");
        Console.WriteLine(xmpXml);
    }
}