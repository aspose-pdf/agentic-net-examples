using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet_stream_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open input and output streams; ensure they are disposed properly
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does NOT implement IDisposable, so instantiate normally
            PdfFileEditor editor = new PdfFileEditor();

            // Create a booklet with A5 page size
            editor.MakeBooklet(inputStream, outputStream, PageSize.A5);
        }

        Console.WriteLine($"Booklet created: {outputPath}");
    }
}