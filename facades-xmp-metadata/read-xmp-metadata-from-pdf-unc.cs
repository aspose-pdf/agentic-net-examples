using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // UNC path to the PDF file on a network share
        const string pdfPath = @"\\server\share\documents\sample.pdf";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use the PdfXmpMetadata facade to read XMP metadata
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the PDF located at the UNC path
            xmp.BindPdf(pdfPath);

            // Retrieve the entire XMP metadata as an XML byte array
            byte[] xmlBytes = xmp.GetXmpMetadata();

            // Convert the byte array to a UTF‑8 string for display or further processing
            string xml = Encoding.UTF8.GetString(xmlBytes);

            Console.WriteLine("XMP Metadata (XML):");
            Console.WriteLine(xml);
        }
    }
}