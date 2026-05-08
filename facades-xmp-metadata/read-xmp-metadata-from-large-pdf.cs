using System;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "large.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Start timing
        Stopwatch sw = Stopwatch.StartNew();

        // Load PDF and read XMP metadata using the Facade
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);                 // Load the PDF
            byte[] data = xmp.GetXmpMetadata();     // Retrieve XMP as XML bytes

            // Optional: display size of the metadata
            Console.WriteLine($"XMP metadata size: {data.Length} bytes");
        }

        // Stop timing
        sw.Stop();
        Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} ms");
    }
}