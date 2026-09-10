using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "large_input.pdf";
        const string outputPathPath = "resized_path.pdf";
        const string outputPathStream = "resized_stream.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Resize all pages; null means all pages
        int[] pages = null;

        // Example new dimensions (in default space units)
        double newWidth = 500;
        double newHeight = 700;

        // Measure performance of the file‑path overload
        Stopwatch swPath = Stopwatch.StartNew();
        PdfFileEditor editorPath = new PdfFileEditor();
        editorPath.ResizeContents(inputPath, outputPathPath, pages, newWidth, newHeight);
        swPath.Stop();
        Console.WriteLine($"Resize using file path overload: {swPath.ElapsedMilliseconds} ms");

        // Measure performance of the stream overload
        Stopwatch swStream = Stopwatch.StartNew();
        using (FileStream inStream = File.OpenRead(inputPath))
        using (FileStream outStream = File.Create(outputPathStream))
        {
            PdfFileEditor editorStream = new PdfFileEditor();
            editorStream.ResizeContents(inStream, outStream, pages, newWidth, newHeight);
        }
        swStream.Stop();
        Console.WriteLine($"Resize using stream overload: {swStream.ElapsedMilliseconds} ms");
    }
}