using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the large PDF (500+ pages)
        const string pdfPath = "large.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Warm‑up to exclude JIT compilation from measurement
        ExtractXmpMetadata(pdfPath);

        // Measure the time required to read XMP metadata
        Stopwatch sw = Stopwatch.StartNew();
        string xmp = ExtractXmpMetadata(pdfPath);
        sw.Stop();

        Console.WriteLine($"XMP metadata length: { (xmp?.Length ?? 0) } characters");
        Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} ms");
    }

    // Helper that extracts XMP metadata using Aspose.Pdf.Document
    static string ExtractXmpMetadata(string path)
    {
        // Document implements IDisposable, so we use a using block
        using (var doc = new Document(path))
        {
            // XMP metadata is stored in the Metadata dictionary under the key "XmpMetadata"
            if (doc.Metadata != null && doc.Metadata.TryGetValue("XmpMetadata", out var value))
            {
                return value?.ToString() ?? string.Empty;
            }
            return string.Empty;
        }
    }
}
