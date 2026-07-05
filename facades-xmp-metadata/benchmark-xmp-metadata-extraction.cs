using System;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the large PDF (500+ pages)
        const string pdfPath = "large_document.pdf";

        // Verify the file exists before proceeding
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Stopwatch for benchmarking
        Stopwatch sw = new Stopwatch();

        // Create the XMP metadata facade
        PdfXmpMetadata xmp = new PdfXmpMetadata();

        // Start timing
        sw.Start();

        // Bind the PDF file to the facade
        xmp.BindPdf(pdfPath);

        // Retrieve the full XMP metadata as a byte array
        byte[] metadataBytes = xmp.GetXmpMetadata();

        // Stop timing
        sw.Stop();

        // Output benchmark result
        Console.WriteLine($"XMP metadata extraction time: {sw.ElapsedMilliseconds} ms");

        // Optionally display size of the metadata (in bytes)
        Console.WriteLine($"Metadata size: {metadataBytes.Length} bytes");

        // Clean up (PdfXmpMetadata does not implement IDisposable, so no explicit disposal needed)
    }
}