using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "large_input.pdf";          // source PDF (must exist)
        const string outputFilePath = "resized_by_path.pdf";    // result of file‑path overload
        const string outputStreamPath = "resized_by_stream.pdf";// result of stream overload

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Resize using the file‑path overload of PdfPageEditor.BindPdf
        // -----------------------------------------------------------------
        long filePathMilliseconds;
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            var sw = Stopwatch.StartNew();

            // Bind the PDF by file path
            editor.BindPdf(inputPdfPath);

            // Example resize: shrink to 50 % of original size
            editor.Zoom = 0.5f; // float literal required

            // Save the resized PDF (file‑path overload)
            editor.Save(outputFilePath);

            sw.Stop();
            filePathMilliseconds = sw.ElapsedMilliseconds;
        }
        Console.WriteLine($"Resize via file path took {filePathMilliseconds} ms");

        // -----------------------------------------------------------------
        // Resize using the stream overload of PdfPageEditor.BindPdf
        // -----------------------------------------------------------------
        long streamMilliseconds;
        using (FileStream inputStream = File.OpenRead(inputPdfPath))
        using (FileStream outputStream = File.Create(outputStreamPath))
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            var sw = Stopwatch.StartNew();

            // Bind the PDF by stream
            editor.BindPdf(inputStream);

            // Same resize operation
            editor.Zoom = 0.5f; // float literal required

            // Save the resized PDF to an output stream
            editor.Save(outputStream);

            sw.Stop();
            streamMilliseconds = sw.ElapsedMilliseconds;
        }
        Console.WriteLine($"Resize via stream took {streamMilliseconds} ms");

        // Simple comparison output
        if (filePathMilliseconds < streamMilliseconds)
            Console.WriteLine("File‑path overload was faster.");
        else if (streamMilliseconds < filePathMilliseconds)
            Console.WriteLine("Stream overload was faster.");
        else
            Console.WriteLine("Both overloads took the same time.");
    }
}