using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the output PDF that will contain the extracted pages
        const string outputPath = "stream_output.pdf";

        // Path to the source PDF (replace with your actual source or obtain the stream from elsewhere)
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream inputStream = File.OpenRead(inputPath))
        // Create the destination stream for the extracted pages
        using (FileStream outputStream = File.Create(outputPath))
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it normally
            PdfFileEditor editor = new PdfFileEditor();

            // Extract pages 3 through 7 (inclusive) from the input stream into the output stream
            // Correct overload: Extract(Stream sourcePdfStream, int startPage, int endPage, Stream outputPdfStream)
            editor.Extract(inputStream, 3, 7, outputStream);
        }

        Console.WriteLine($"Pages 3‑7 extracted to '{outputPath}'.");
    }
}