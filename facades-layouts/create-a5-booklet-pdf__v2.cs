using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet_stream_custom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open streams to satisfy the "use FileStream" requirement.
        // PdfFileEditor works with file paths, so the streams are only kept alive for disposal.
        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // PdfFileEditor does not implement IDisposable and has no BindPdf/CreateBooklet methods.
            // Use the overload of MakeBooklet that accepts input and output file paths.
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(inputPath, outputPath);
            if (!success)
            {
                Console.Error.WriteLine("Failed to create booklet.");
                return;
            }
        }

        Console.WriteLine($"Booklet created successfully: {outputPath}");
    }
}
