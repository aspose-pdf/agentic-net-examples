using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";      // source PDF file
        const string outputPath = "stream_range.pdf"; // destination PDF file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Use stream‑based overloads of PdfFileEditor.Extract.
        using var inputStream = File.OpenRead(inputPath);
        using var outputStream = File.Create(outputPath);
        var editor = new PdfFileEditor();

        // Extract pages 15 through 25 (inclusive) from the source PDF to the destination stream.
        editor.Extract(inputStream, 15, 25, outputStream);

        Console.WriteLine($"Pages 15‑25 extracted to '{outputPath}'.");
    }
}
