using System;
using System.IO;
using System.Text;
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

        try
        {
            // Initialize the XMP metadata facade and bind the PDF file
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(pdfPath);

                // Retrieve the raw XMP metadata as a byte array
                byte[] rawData = xmp.GetXmpMetadata();

                // Convert the byte array to a UTF‑8 string
                string xmpXml = Encoding.UTF8.GetString(rawData);

                // Output the XML string
                Console.WriteLine("XMP Metadata:");
                Console.WriteLine(xmpXml);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}