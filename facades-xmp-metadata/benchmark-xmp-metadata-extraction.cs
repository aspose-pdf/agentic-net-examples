using System;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "large_input.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Warm‑up: bind once to avoid first‑time overhead affecting the measurement
        using (PdfXmpMetadata warmup = new PdfXmpMetadata())
        {
            warmup.BindPdf(inputPdf);
            warmup.GetXmpMetadata();
        }

        // Benchmark the XMP metadata extraction
        Stopwatch sw = Stopwatch.StartNew();

        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);
            byte[] metadataBytes = xmp.GetXmpMetadata(); // read full XMP metadata
            // Optionally, you could process the bytes here (e.g., convert to string)
            // string xml = System.Text.Encoding.UTF8.GetString(metadataBytes);
        }

        sw.Stop();

        Console.WriteLine($"XMP metadata extraction time: {sw.ElapsedMilliseconds} ms");
    }
}