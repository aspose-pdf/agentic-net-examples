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

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Use the PdfXmpMetadata facade to read XMP metadata
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(pdfPath);                     // Initialize with the PDF file
                byte[] rawData = xmp.GetXmpMetadata();    // Retrieve XMP as XML bytes
                string xml = Encoding.UTF8.GetString(rawData);
                Console.WriteLine("XMP Metadata:");
                Console.WriteLine(xml);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading XMP metadata: {ex.Message}");
        }
    }
}