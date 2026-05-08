using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "large.pdf";
        const string outputPathFile = "resized_file.pdf";
        const string outputPathStream = "resized_stream.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Measure performance using the file‑path overload
        var swFile = Stopwatch.StartNew();
        using (var editor = new PdfPageEditor())
        {
            // Bind the source PDF via file path
            editor.BindPdf(inputPath);
            // Shrink the page content to 50 %
            editor.Zoom = 0.5f;
            // Save the resized document
            editor.Save(outputPathFile);
        }
        swFile.Stop();
        Console.WriteLine($"File‑path overload elapsed: {swFile.ElapsedMilliseconds} ms");

        // Measure performance using the stream overload
        var swStream = Stopwatch.StartNew();
        using (FileStream inputStream = File.OpenRead(inputPath))
        using (var editor = new PdfPageEditor())
        {
            // Bind the source PDF via stream
            editor.BindPdf(inputStream);
            // Same resize operation
            editor.Zoom = 0.5f;
            // Save the resized document
            editor.Save(outputPathStream);
        }
        swStream.Stop();
        Console.WriteLine($"Stream overload elapsed: {swStream.ElapsedMilliseconds} ms");
    }
}
