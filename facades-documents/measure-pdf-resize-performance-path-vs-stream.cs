using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "large_input.pdf";
        const string outputPathFile = "resized_by_path.pdf";
        const string outputPathStream = "resized_by_stream.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Resize using file‑path overload
        long timePath;
        var editorPath = new PdfFileEditor(); // PdfFileEditor does NOT implement IDisposable
        Stopwatch sw = Stopwatch.StartNew();
        editorPath.ResizeContents(inputPdfPath, outputPathFile, null, 0, 0);
        sw.Stop();
        timePath = sw.ElapsedMilliseconds;

        // Resize using stream overload
        long timeStream;
        using (FileStream inputStream = File.OpenRead(inputPdfPath))
        using (FileStream outputStream = File.Create(outputPathStream))
        {
            var editorStream = new PdfFileEditor(); // direct instantiation, no using
            Stopwatch sw2 = Stopwatch.StartNew();
            editorStream.ResizeContents(inputStream, outputStream, null, 0, 0);
            sw2.Stop();
            timeStream = sw2.ElapsedMilliseconds;
        }

        Console.WriteLine($"Resize using file path took   {timePath} ms");
        Console.WriteLine($"Resize using streams took      {timeStream} ms");
    }
}
