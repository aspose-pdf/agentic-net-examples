using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // UNC path to the PDF file on a network share
        const string pdfPath = @"\\ServerName\ShareFolder\Documents\sample.pdf";

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
                // Bind the PDF located at the UNC path
                xmp.BindPdf(pdfPath);

                // Retrieve the entire XMP metadata as a byte array (XML format)
                byte[] rawData = xmp.GetXmpMetadata();

                // Convert the byte array to a UTF‑8 string for display or further processing
                string xmlMetadata = Encoding.UTF8.GetString(rawData);

                Console.WriteLine("XMP Metadata (XML):");
                Console.WriteLine(xmlMetadata);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading XMP metadata: {ex.Message}");
        }
    }
}