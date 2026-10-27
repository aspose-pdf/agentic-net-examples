using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "large.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Measure the time required to read XMP metadata
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Use the PdfXmpMetadata facade (Aspose.Pdf.Facades) to read XMP metadata
        var xmpFacade = new PdfXmpMetadata();
        // GetXmpMetadata returns a byte[]; convert it to a UTF‑8 string
        byte[] xmpBytes = xmpFacade.GetXmpMetadata(pdfPath);
        string xmpMetadata = xmpBytes != null ? Encoding.UTF8.GetString(xmpBytes) : null;

        stopwatch.Stop();

        Console.WriteLine($"XMP metadata length: {(xmpMetadata?.Length ?? 0)} characters");
        Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
    }
}
