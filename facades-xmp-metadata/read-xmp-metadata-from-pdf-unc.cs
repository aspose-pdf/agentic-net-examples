using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // UNC path to the PDF file on the network share
        const string pdfPath = @"\\server\share\documents\sample.pdf";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use the PdfXmpMetadata facade to bind the PDF and retrieve XMP metadata
        using (PdfXmpMetadata xmpMetadata = new PdfXmpMetadata())
        {
            // Bind the PDF located at the UNC path
            xmpMetadata.BindPdf(pdfPath);

            // Retrieve the entire XMP metadata as a byte array (XML format)
            byte[] fullMetadataBytes = xmpMetadata.GetXmpMetadata();
            string fullMetadataXml = Encoding.UTF8.GetString(fullMetadataBytes);

            Console.WriteLine("=== Full XMP Metadata ===");
            Console.WriteLine(fullMetadataXml);
            Console.WriteLine();

            // Example: retrieve a specific XMP property (e.g., creator)
            byte[] creatorBytes = xmpMetadata.GetXmpMetadata("dc:creator");
            string creatorXml = Encoding.UTF8.GetString(creatorBytes);

            Console.WriteLine("=== dc:creator Metadata ===");
            Console.WriteLine(creatorXml);
        }
    }
}