using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet_stream_default.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create file streams for input and output PDFs.
        // Streams are wrapped in using blocks for deterministic disposal.
        using (FileStream inputStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable, so it is NOT wrapped in a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // Convert the input PDF into a booklet layout.
            // This overload works directly with streams.
            editor.MakeBooklet(inputStream, outputStream);
        }

        Console.WriteLine($"Booklet PDF created: {outputPath}");
    }
}