using System;
using System.Diagnostics;
using System.IO;
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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Measure performance using the file‑path overload
        Stopwatch swPath = Stopwatch.StartNew();
        using (PdfPageEditor editorPath = new PdfPageEditor())
        {
            // Bind using file path
            editorPath.BindPdf(inputPath);
            // Example resize: shrink to 50%
            editorPath.Zoom = 0.5f;
            editorPath.ApplyChanges();
            editorPath.Save(outputPathPath);
        }
        swPath.Stop();
        Console.WriteLine($"File‑path overload elapsed: {swPath.ElapsedMilliseconds} ms");

        // Measure performance using the stream overload
        Stopwatch swStream = Stopwatch.StartNew();
        using (FileStream fs = File.OpenRead(inputPath))
        using (PdfPageEditor editorStream = new PdfPageEditor())
        {
            // Bind using stream
            editorStream.BindPdf(fs);
            // Same resize operation
            editorStream.Zoom = 0.5f;
            editorStream.ApplyChanges();
            editorStream.Save(outputPathStream);
        }
        swStream.Stop();
        Console.WriteLine($"Stream overload elapsed: {swStream.ElapsedMilliseconds} ms");
    }
}