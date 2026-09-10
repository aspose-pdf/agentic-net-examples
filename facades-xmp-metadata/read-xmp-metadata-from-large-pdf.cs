using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "large_input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Measure the time taken to bind the PDF and retrieve XMP metadata
        Stopwatch sw = new Stopwatch();

        // PdfXmpMetadata implements SaveableFacade (IDisposable), so use using for deterministic disposal
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            sw.Start();

            // Bind the PDF file to the facade
            xmp.BindPdf(pdfPath);

            // Retrieve the full XMP metadata as a byte array
            byte[] metadataBytes = xmp.GetXmpMetadata();

            sw.Stop();

            // Convert metadata to a UTF-8 string for display (optional)
            string metadataXml = System.Text.Encoding.UTF8.GetString(metadataBytes);

            Console.WriteLine("XMP Metadata (excerpt):");
            Console.WriteLine(metadataXml.Length > 500 ? metadataXml.Substring(0, 500) + "..." : metadataXml);
            Console.WriteLine();
            Console.WriteLine($"Time elapsed: {sw.Elapsed.TotalMilliseconds} ms");
        }
    }
}