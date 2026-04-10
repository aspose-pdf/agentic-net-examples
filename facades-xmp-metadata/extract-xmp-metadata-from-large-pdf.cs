using System;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "large.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Start timing the XMP metadata extraction
        Stopwatch sw = Stopwatch.StartNew();

        // Bind the PDF and read its XMP metadata using the facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(pdfPath);
            byte[] data = xmp.GetXmpMetadata(); // Retrieves the full XMP packet as XML bytes

            // Example processing: convert to string and display size
            string xml = System.Text.Encoding.UTF8.GetString(data);
            Console.WriteLine($"XMP metadata size: {data.Length} bytes");
        }

        sw.Stop();
        Console.WriteLine($"Extraction time: {sw.ElapsedMilliseconds} ms");
    }
}