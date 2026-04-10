using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class PdfResizePerformance
{
    static void Main()
    {
        const string inputPdf = "large_input.pdf";
        const string outputFilePath = "resized_by_path.pdf";
        const string outputStreamPath = "resized_by_stream.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Measure performance using the file‑path overload
        long filePathDuration;
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            Stopwatch sw = Stopwatch.StartNew();

            // Bind using file path
            editor.BindPdf(inputPdf);

            // Example resize: shrink to 50% of original size
            editor.Zoom = 0.5f; // float literal

            // Apply changes and save
            editor.ApplyChanges();
            editor.Save(outputFilePath);

            sw.Stop();
            filePathDuration = sw.ElapsedMilliseconds;
        }

        // Measure performance using the stream overload
        long streamDuration;
        using (PdfPageEditor editor = new PdfPageEditor())
        using (FileStream inputStream = File.OpenRead(inputPdf))
        using (FileStream outputStream = File.Create(outputStreamPath))
        {
            Stopwatch sw = Stopwatch.StartNew();

            // Bind using stream
            editor.BindPdf(inputStream);

            // Same resize operation
            editor.Zoom = 0.5f; // float literal

            // Apply changes and save to stream
            editor.ApplyChanges();
            editor.Save(outputStream);

            sw.Stop();
            streamDuration = sw.ElapsedMilliseconds;
        }

        Console.WriteLine($"Resize using file path took: {filePathDuration} ms");
        Console.WriteLine($"Resize using stream took:    {streamDuration} ms");
    }
}
