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

        // Bind the PDF file and retrieve its XMP metadata as raw XML bytes
        using (PdfXmpMetadata xmpMetadata = new PdfXmpMetadata())
        {
            xmpMetadata.BindPdf(pdfPath);
            byte[] rawData = xmpMetadata.GetXmpMetadata(); // XML in byte[] form

            // Convert the byte array to a UTF‑8 string for display or further processing
            string xml = System.Text.Encoding.UTF8.GetString(rawData);
            Console.WriteLine(xml);
        }
    }
}