using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // UNC path to the PDF file on a network share
        const string pdfPath = @"\\server\share\folder\document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfXmpMetadata implements IDisposable, so use a using block for deterministic cleanup
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                // Bind the PDF file (can be a UNC path) to the facade
                xmp.BindPdf(pdfPath);

                // Retrieve the entire XMP metadata as a byte array (XML format)
                byte[] rawData = xmp.GetXmpMetadata();

                // Convert the byte array to a UTF‑8 string for display or further processing
                string xmpXml = Encoding.UTF8.GetString(rawData);

                Console.WriteLine("XMP Metadata:");
                Console.WriteLine(xmpXml);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading XMP metadata: {ex.Message}");
        }
    }
}